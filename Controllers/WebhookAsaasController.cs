using ADUSAPI.Services;
using ADUSAPICore.Models.Checkout;
using ADUSAPICore.Models.Parcela;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ADUSAPICore.Models.Enum;
using ADUSAPI.Shared;
using ADUSAPICore.Models.MovimentoCaixa;
using Microsoft.Extensions.Options;
using ADUSAPI.Entities;
using System.Data;
using System.Drawing;
using System;
using ADUSAPI.Migrations;
using ADUSAPICore.Models.Assinatura;
using Microsoft.AspNetCore.Authorization;

namespace ADUSAPI.Controllers
{
    [ApiController]
    [Route("api/webhookasaas")]
    [Authorize]
    public class WebhookAsaasController : ControllerBase
    {
        private readonly LogCheckoutService _logService;
        private readonly ParcelaService _parcelaService;
        private readonly BuscarParceiroPorCustomerAsaasService _buscaparceiro;
        private readonly MovimentoCaixaService _movcaixa;
        private readonly ASAASSettings _asaasSettings;
        private readonly AssinaturaService _assinatura;
        private readonly CartaoAssinaturaService _cartao;

        public WebhookAsaasController(LogCheckoutService logService, ParcelaService parcelaService, BuscarParceiroPorCustomerAsaasService buscaparceiro, MovimentoCaixaService movcaixa, IOptions<ASAASSettings> asaasSettings, AssinaturaService assinatura, CartaoAssinaturaService cartao)
        {
            _logService = logService;
            _parcelaService = parcelaService;
            _buscaparceiro = buscaparceiro;
            _movcaixa = movcaixa;
            _asaasSettings = asaasSettings.Value;
            _assinatura = assinatura;
            _cartao = cartao;
        }

        [HttpPost]
        public async Task<IActionResult> ReceberWebhook([FromBody] JsonElement payload)
        {
            //            string jsonRecebido = payload.ToString();
            // Aqui você pode gravar o log ou processar o evento
            //          Console.WriteLine("Webhook recebido: " + jsonRecebido);

            //          return Ok();

            var root = payload;

            string eventType = root.GetProperty("event").GetString() ?? "Unknown";
            if (eventType != "SUBSCRIPTION_CREATED")
            {
                try
                {
                    string paymentId = root.GetProperty("payment").GetProperty("id").GetString();
                    string subscriptionId = root.GetProperty("payment").TryGetProperty("subscription", out var sub) ? sub.GetString() : null;
                    bool basaas = false;
                    basaas = (subscriptionId != null);
                    if (subscriptionId == null)
                    {
                        subscriptionId = root.GetProperty("payment").TryGetProperty("externalReference", out var xsub) ? xsub.GetString() : null;
                    }
                    string customerId = root.GetProperty("payment").GetProperty("customer").GetString();
                    string idParceiro = await _buscaparceiro.BuscarIdParceiroPorCustomerId(customerId);
                    if (idParceiro != null)
                    {
                        decimal value = root.GetProperty("payment").GetProperty("value").GetDecimal();
                        decimal netvalue = root.GetProperty("payment").GetProperty("netValue").GetDecimal();
                        string status = root.GetProperty("payment").GetProperty("status").GetString();
                        string billingType = root.GetProperty("payment").GetProperty("billingType").GetString();

                        //item.TryGetProperty("invoiceUrl", out var url) ? url.GetString() : null,
                        int numparcela = root.GetProperty("payment").TryGetProperty("installmentNumber", out var nump) && nump.ValueKind == JsonValueKind.Number ? nump.GetInt32() : 0;
                        DateTime dueDate = root.GetProperty("payment").GetProperty("dueDate").GetDateTime();
                        DateTime? estimatedCreditDate = root.GetProperty("payment").TryGetProperty("estimatedCreditDate", out var ec) && ec.ValueKind == JsonValueKind.String
                            ? ec.GetDateTime()
                            : null;
                        DateTime? paymentDate = root.GetProperty("payment").TryGetProperty("clientPaymentDate", out var ep) && ep.ValueKind == JsonValueKind.String
                            ? ep.GetDateTime()
                            : null;

                        JsonElement[] comissoes = Array.Empty<JsonElement>();

                        if (root.TryGetProperty("split", out var splitElement) && splitElement.ValueKind == JsonValueKind.Array)
                        {
                            comissoes = splitElement.EnumerateArray().ToArray();
                        }

                        // 👉 Grava o LOG completo
                        var log = new LogCheckoutViewModel
                        {
                            NomeCliente = customerId,
                            IpOrigem = HttpContext.Connection.RemoteIpAddress?.ToString(),
                            TipoOperacao = eventType,
                            UrlRequisicao = "/api/webhookasaas",
                            PayloadEnviado = " ",
                            RetornoApi = " ",
                            StatusHttp = "200 OK",
                            Erro = null
                        };

                        await _logService.Adicionar(log);

                        if (billingType != "CREDIT_CARD")
                        {
                            if (eventType == "PAYMENT_CREATED" && basaas)
                            {
                                var p = await _parcelaService.ListarParcelaByIdCheckout(paymentId);
                                var sb = await _assinatura.ListarAssinaturaByIdPlataforma(subscriptionId);
                                if (p == null && sb != null)
                                {
                                    numparcela = numparcela == 0 ? await _parcelaService.GetParcela(sb.id) : numparcela;
                                    var parcela = new ParcelaViewModel
                                    {
                                        id = Guid.NewGuid().ToString(),
                                        idassinatura = sb.id,
                                        idcheckout = paymentId,
                                        nossonumero = paymentId,
                                        idparceiro = idParceiro,
                                        datavencimento = dueDate,
                                        valor = value,
                                        valorliquido = value,
                                        plataforma = "Asaas",
                                        idformapagto = billingType switch
                                        {
                                            "PIX" => FormaPagto.Pix,
                                            "BOLETO" => FormaPagto.Boleto,
                                            _ => FormaPagto.Cartao
                                        },
                                        dataestimadapagto = estimatedCreditDate,
                                        numparcela = numparcela, // Você pode ajustar depois com base no installmentNumber, se quiser
                                        comissao = (decimal)0.10 * value,
                                        acrescimos = 0,
                                        descontoantecipacao = 0,
                                        descontoplataforma = value - netvalue,
                                        descontos = 0
                                    };

                                    await _parcelaService.AdicionarParcela(parcela);
                                }
                                else
                                {
                                    log = new LogCheckoutViewModel
                                    {
                                        NomeCliente = customerId,
                                        IpOrigem = HttpContext.Connection.RemoteIpAddress?.ToString(),
                                        TipoOperacao = eventType,
                                        UrlRequisicao = "/api/webhookasaas",
                                        PayloadEnviado = " ",
                                        RetornoApi = " ",
                                        StatusHttp = "501",
                                        Erro = "Assinatura nao encontrada " + subscriptionId
                                    };

                                    return StatusCode(501, "Assinatura não encontrada " + subscriptionId);
                                }
                            }
                            else
                            {
                                if (eventType == "PAYMENT_CONFIRMED" || eventType == "PAYMENT_RECEIVED")
                                {
                                    var p = await _parcelaService.ListarParcelaByIdCheckout(paymentId);
                                    p.databaixa = paymentDate;
                                    p.dataestimadapagto = estimatedCreditDate;
                                    if (p.idcaixa == 0 || p.idcaixa == null)
                                    {
                                        var respc = await _movcaixa.AdicionarAsync(new MovimentoCaixaViewModel
                                        {
                                            DataMov = root.GetProperty("payment").GetProperty("creditDate").GetDateTime().Date,
                                            IdCategoria = _asaasSettings.idcategoria,
                                            IdCentroCusto = _asaasSettings.idccusto,
                                            IdContaCorrente = _asaasSettings.idconta,
                                            IdTransacao = _asaasSettings.idtransacao,
                                            idparceiro = idParceiro,
                                            Valor = root.GetProperty("payment").GetProperty("value").GetDecimal(),
                                            Sinal = "C",
                                            Observacao = "RECEBIMENTO ASAAS",
                                            idmovbanco = root.GetProperty("payment").GetProperty("id").GetString()
                                        });
                                        p.idcaixa = respc.Id;
                                        var x = await _parcelaService.SalvarParcela(p.id, p);

                                        if (comissoes.Length > 0)
                                        {
                                            foreach (var com in comissoes)
                                            {
                                                var respcom = await _movcaixa.AdicionarAsync(new MovimentoCaixaViewModel
                                                {
                                                    DataMov = root.GetProperty("payment").GetProperty("creditDate").GetDateTime().Date,
                                                    IdCategoria = _asaasSettings.idcategoriacomiss,
                                                    IdCentroCusto = _asaasSettings.idccusto,
                                                    IdContaCorrente = _asaasSettings.idconta,
                                                    IdTransacao = _asaasSettings.idtransacaocomiss,
                                                    idparceiro = idParceiro,
                                                    Valor = com.GetProperty("totalValue").GetDecimal(),
                                                    Sinal = "D",
                                                    Observacao = "PAGTO COMISSAO",
                                                    idmovbanco = root.GetProperty("payment").GetProperty("id").GetString()
                                                });
                                            }
                                        }
                                        //taxa
                                        if (root.GetProperty("payment").GetProperty("value").GetDecimal() != netvalue)
                                        {
                                            var resptx = await _movcaixa.AdicionarAsync(new MovimentoCaixaViewModel
                                            {
                                                DataMov = root.GetProperty("payment").GetProperty("creditDate").GetDateTime().Date,
                                                IdCategoria = _asaasSettings.idcategoriataxa,
                                                IdCentroCusto = _asaasSettings.idccusto,
                                                IdContaCorrente = _asaasSettings.idconta,
                                                IdTransacao = _asaasSettings.idtransacaotaxa,
                                                idparceiro = idParceiro,
                                                Valor = root.GetProperty("payment").GetProperty("value").GetDecimal() - netvalue,
                                                Sinal = "D",
                                                Observacao = "TAXA PLATAFORMA",
                                                idmovbanco = root.GetProperty("payment").GetProperty("id").GetString()
                                            });
                                        }
                                    }
                                    else
                                    {
                                        var x = await _parcelaService.SalvarParcela(p.id, p);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (eventType == "PAYMENT_CREATED" && (basaas || numparcela > 0))
                            {
                                AssinaturaViewModel ass;
                                if (!basaas)
                                {
                                    ass = await _assinatura.ListarAssinaturaById(subscriptionId);
                                }
                                else
                                {
                                    ass = await _assinatura.ListarAssinaturaByIdPlataforma(subscriptionId);
                                }
                                if (ass != null)
                                {
                                    var p = await _parcelaService.ListarParcelaByIdCheckout(paymentId);
                                    numparcela = numparcela == 0 ? await _parcelaService.GetParcela(ass.id) : numparcela;
                                    if (p == null)
                                    {
                                        var parcela = new ParcelaViewModel
                                        {
                                            id = paymentId,
                                            idassinatura = ass.id,
                                            idcheckout = paymentId,
                                            nossonumero = paymentId,
                                            idparceiro = idParceiro,
                                            datavencimento = dueDate,
                                            valor = value,
                                            valorliquido = value,
                                            plataforma = "Asaas",
                                            idformapagto = billingType switch
                                            {
                                                "PIX" => FormaPagto.Pix,
                                                "BOLETO" => FormaPagto.Boleto,
                                                _ => FormaPagto.Cartao
                                            },
                                            dataestimadapagto = estimatedCreditDate,
                                            numparcela = numparcela, // Você pode ajustar depois com base no installmentNumber, se quiser
                                            comissao = (decimal)0.10 * value,
                                            acrescimos = 0,
                                            descontoantecipacao = 0,
                                            descontoplataforma = value - netvalue,
                                            descontos = 0
                                        };

                                        await _parcelaService.AdicionarParcela(parcela);
                                    }
                                }
                            }
                            else
                            {
                                if (eventType == "PAYMENT_CONFIRMED")

                                {
                                    AssinaturaViewModel ass;
                                    if (!basaas)
                                    {
                                        ass = await _assinatura.ListarAssinaturaById(subscriptionId);
                                    }
                                    else
                                    {
                                        ass = await _assinatura.ListarAssinaturaByIdPlataforma(subscriptionId);
                                    }
                                    if (ass != null)
                                    {
                                        var p = await _parcelaService.ListarParcelaByIdCheckout(paymentId);
                                        if (p != null)
                                        {
                                            p.databaixa = paymentDate;
                                            var x = await _parcelaService.SalvarParcela(p.id, p);
                                        }
                                        /*
                                        else
                                        {
                                            var parcela = new ParcelaViewModel
                                            {
                                                id = paymentId,
                                                idassinatura = subscriptionId,
                                                idcheckout = paymentId,
                                                nossonumero = paymentId,
                                                idparceiro = idParceiro,
                                                datavencimento = dueDate,
                                                valor = value,
                                                valorliquido = netvalue - (decimal)0.10 * value,
                                                databaixa = paymentDate,
                                                plataforma = "Asaas",
                                                idformapagto = billingType switch
                                                {
                                                    "PIX" => FormaPagto.Pix,
                                                    "BOLETO" => FormaPagto.Boleto,
                                                    _ => FormaPagto.Cartao
                                                },
                                                dataestimadapagto = estimatedCreditDate,
                                                numparcela = numparcela, // Você pode ajustar depois com base no installmentNumber, se quiser
                                                comissao = (decimal)0.10 * value,
                                                acrescimos = 0,
                                                descontoantecipacao = 0,
                                                descontoplataforma = value - netvalue,
                                                descontos = 0
                                            };

                                            await _parcelaService.AdicionarParcela(parcela);
                                        }
                                        */
                                    }
                                }
                                else
                                {
                                    if (eventType == "PAYMENT_RECEIVED")
                                    {
                                        AssinaturaViewModel ass;
                                        if (!basaas)
                                        {
                                            ass = await _assinatura.ListarAssinaturaById(subscriptionId);
                                        }
                                        else
                                        {
                                            ass = await _assinatura.ListarAssinaturaByIdPlataforma(subscriptionId);
                                        }

                                        if (ass != null)
                                        {
                                            var p = await _parcelaService.ListarParcelaByIdCheckout(paymentId);
                                            p.dataestimadapagto = estimatedCreditDate;
                                            if (p.idcaixa == 0 || p.idcaixa == null)
                                            {
                                                //Recebimento
                                                var respc = await _movcaixa.AdicionarAsync(new MovimentoCaixaViewModel
                                                {
                                                    DataMov = root.GetProperty("payment").GetProperty("creditDate").GetDateTime().Date,
                                                    IdCategoria = _asaasSettings.idcategoria,
                                                    IdCentroCusto = _asaasSettings.idccusto,
                                                    IdContaCorrente = _asaasSettings.idconta,
                                                    IdTransacao = _asaasSettings.idtransacao,
                                                    idparceiro = idParceiro,
                                                    Valor = root.GetProperty("payment").GetProperty("value").GetDecimal(),
                                                    Sinal = "C",
                                                    Observacao = "RECEBIMENTO ASAAS",
                                                    idmovbanco = root.GetProperty("payment").GetProperty("id").GetString()
                                                });
                                                p.idcaixa = respc.Id;
                                                var x = await _parcelaService.SalvarParcela(p.id, p);

                                                //Comissoes
                                                if (comissoes.Length > 0)
                                                {
                                                    foreach (var com in comissoes)
                                                    {
                                                        var respcom = await _movcaixa.AdicionarAsync(new MovimentoCaixaViewModel
                                                        {
                                                            DataMov = root.GetProperty("payment").GetProperty("creditDate").GetDateTime().Date,
                                                            IdCategoria = _asaasSettings.idcategoriacomiss,
                                                            IdCentroCusto = _asaasSettings.idccusto,
                                                            IdContaCorrente = _asaasSettings.idconta,
                                                            IdTransacao = _asaasSettings.idtransacaocomiss,
                                                            idparceiro = _asaasSettings.idparceiro,
                                                            Valor = com.GetProperty("totalValue").GetDecimal(),
                                                            Sinal = "D",
                                                            Observacao = "PAGTO COMISSAO",
                                                            idmovbanco = root.GetProperty("payment").GetProperty("id").GetString()
                                                        });
                                                    }
                                                }

                                                //Taxa
                                                if (root.GetProperty("payment").GetProperty("value").GetDecimal() != netvalue)
                                                {
                                                    var resptx = await _movcaixa.AdicionarAsync(new MovimentoCaixaViewModel
                                                    {
                                                        DataMov = root.GetProperty("payment").GetProperty("creditDate").GetDateTime().Date,
                                                        IdCategoria = _asaasSettings.idcategoriataxa,
                                                        IdCentroCusto = _asaasSettings.idccusto,
                                                        IdContaCorrente = _asaasSettings.idconta,
                                                        IdTransacao = _asaasSettings.idtransacaotaxa,
                                                        idparceiro = idParceiro,
                                                        Valor = root.GetProperty("payment").GetProperty("value").GetDecimal() - netvalue,
                                                        Sinal = "D",
                                                        Observacao = "TAXA PLATAFORMA",
                                                        idmovbanco = root.GetProperty("payment").GetProperty("id").GetString()
                                                    });
                                                }
                                            }
                                            else
                                            {
                                                var x = await _parcelaService.SalvarParcela(p.id, p);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        return Ok();
                    }
                    else
                    {
                        var log = new LogCheckoutViewModel
                        {
                            NomeCliente = customerId,
                            IpOrigem = HttpContext.Connection.RemoteIpAddress?.ToString(),
                            TipoOperacao = eventType,
                            UrlRequisicao = "/api/webhookasaas",
                            PayloadEnviado = " ",
                            RetornoApi = " ",
                            StatusHttp = "502",
                            Erro = "Cliente " + customerId
                        };

                        return StatusCode(200, "Cliente inexistente" + customerId);
                    }
                }
                catch (Exception ex)
                {
                    await _logService.Adicionar(new LogCheckoutViewModel
                    {
                        NomeCliente = "ErroWebhook",
                        IpOrigem = HttpContext.Connection.RemoteIpAddress?.ToString(),
                        TipoOperacao = "Erro ao processar Webhook",
                        UrlRequisicao = "/api/webhookasaas",
                        PayloadEnviado = " ",
                        RetornoApi = " ",
                        StatusHttp = "500",
                        Erro = ex.ToString()
                    });

                    return StatusCode(500, ex.ToString());
                }
            }
            else
            {
                try
                {
                    var evento = payload.GetProperty("event").GetString();

                    if (evento == "SUBSCRIPTION_CREATED")
                    {
                        var assinatura = payload.GetProperty("subscription");

                        var subscriptionId = assinatura.GetProperty("id").GetString();
                        var customerId = assinatura.GetProperty("customer").GetString();
                        var status = assinatura.GetProperty("status").GetString();
                        var value = assinatura.GetProperty("value").GetDecimal();
                        var billingtype = assinatura.GetProperty("billingType").GetString();
                        //var nextDueDate = assinatura.GetProperty("nextDueDate").GetDateTime();
                        string idParceiro = await _buscaparceiro.BuscarIdParceiroPorCustomerId(customerId);
                        string idafiliado = assinatura.TryGetProperty("externalReference", out var sub) ? sub.GetString() : null;
                        string? bandeira, cctoken, ccdigitos;
                        var cartao = assinatura.TryGetProperty("creditCard", out var cc) ? "X" : null;
                        if (cartao != null)
                        {
                            bandeira = assinatura.GetProperty("creditCard").GetProperty("creditCardBrand").GetString();
                            cctoken = assinatura.GetProperty("creditCard").GetProperty("creditCardToken").GetString();
                            ccdigitos = assinatura.GetProperty("creditCard").GetProperty("creditCardNumber").GetString();
                        }
                        else
                        {
                            bandeira = null;
                            cctoken = null;
                            ccdigitos = null;
                        }

                        var ass = await _assinatura.ListarAssinaturaById(subscriptionId);

                        if (ass == null)
                        {
                            FormaPagto idforma = (billingtype == "PIX") ? FormaPagto.Pix : (billingtype == "BOLETO") ? FormaPagto.Boleto : FormaPagto.Cartao;
                            await _assinatura.AdicionarAssinatura(new AssinaturaViewModel
                            {
                                id = subscriptionId,
                                datavenda = DateTime.Now.Date,
                                idformapagto = idforma,
                                idparceiro = idParceiro,
                                idplataforma = subscriptionId,
                                status = (StatusAssinatura)1,
                                preco = 47,
                                qtd = (int)value / 47,
                                valor = (double)value,
                                observacao = " ",
                                plataforma = "ASAAS",
                                idafiliado = idafiliado
                            });
                            if (cctoken != null)
                            {
                                await _cartao.AdicionarAsync(new CartaoAssinaturaViewModel
                                {
                                    Ativo = true,
                                    Bandeira = bandeira,
                                    IdAssinatura = subscriptionId,
                                    IdToken = cctoken,
                                    UltimosDigitos = ccdigitos
                                });
                            }
                        };

                        return Ok(new { message = "Assinatura registrada com sucesso." });
                    }

                    // Outros eventos podem ser tratados aqui

                    return Ok(new { message = "Evento ignorado." });
                }
                catch (Exception ex)
                {
                    // Logar ou notificar erro
                    return BadRequest(new { error = "Erro no processamento do webhook criando assinatura", detalhe = ex.Message });
                }
            }
        }

        /*
        public async Task<string> AddAssinaturaADUS(string id, string idcliente, string idafiliado, decimal valor, string? bandeira, string? token, string? digitos, string formaPagamento)
        {
            FormaPagto idforma = (formaPagamento == "PIX") ? FormaPagto.Pix : (formaPagamento == "BOLETO") ? FormaPagto.Boleto : FormaPagto.Cartao;
            await _assinatura.AdicionarAssinatura(new AssinaturaViewModel
            {
                id = id,
                datavenda = DateTime.Now.Date,
                idformapagto = idforma,
                idparceiro = idcliente,
                idplataforma = id,
                status = (StatusAssinatura)1,
                preco = 47,
                qtd = (int)valor / 47,
                valor = (double)valor,
                observacao = " ",
                plataforma = "ASAAS",
                idafiliado = idafiliado
            });
            if (token != null)
            {
                await _cartao.AdicionarAsync(new CartaoAssinaturaViewModel
                {
                    Ativo = true,
                    Bandeira = bandeira,
                    IdAssinatura = id,
                    IdToken = token,
                    UltimosDigitos = digitos
                });
            }

            return null;
        }
        */
    }
}