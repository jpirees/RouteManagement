using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Models
{
    public class Document : BaseEntity
    {
        [JsonProperty("Data")]
        public DateTime? Data { get; set; }

        [JsonProperty("Status")]
        public string? Status { get; set; }

        [JsonProperty("Auditado")]
        public string? Auditado { get; set; }

        [JsonProperty("CopReverteu")]
        public string? CopReverteu { get; set; }

        [JsonProperty("Log")]
        public string? Log { get; set; }

        [JsonProperty("Pdf")]
        public string? Pdf { get; set; }

        [JsonProperty("Foto")]
        public string? Foto { get; set; }

        [JsonProperty("Contrato")]
        public string? Contrato { get; set; }

        [JsonProperty("Wo")]
        public string? Wo { get; set; }

        [JsonProperty("Os")]
        public string? Os { get; set; }

        [JsonProperty("Assinante")]
        public string? Assinante { get; set; }

        [JsonProperty("Tecnicos")]
        public string? Tecnicos { get; set; }

        [JsonProperty("Login")]
        public string? Login { get; set; }

        [JsonProperty("Matricula")]
        public string? Matricula { get; set; }

        [JsonProperty("Cop")]
        public string? Cop { get; set; }

        [JsonProperty("UltimoAlterar")]
        public string? UltimoAlterar { get; set; }

        [JsonProperty("Local")]
        public string? Local { get; set; }

        [JsonProperty("PontoCasaApt")]
        public string? PontoCasaApt { get; set; }

        [JsonProperty("Cidade")]
        public string? Cidade { get; set; }

        [JsonProperty("Base")]
        public string? Base { get; set; }

        [JsonProperty("Horario")]
        public DateTime? Horario { get; set; }

        [JsonProperty("Segmento")]
        public string? Segmento { get; set; }

        [JsonProperty("Servico")]
        public string? Servico { get; set; }

        [JsonProperty("TipoDeServico")]
        public string? TipoDeServico { get; set; }

        [JsonProperty("TipoOs")]
        public string? TipoOs { get; set; }

        [JsonProperty("GrupoDeServico")]
        public string? GrupoDeServico { get; set; }

        [JsonProperty("Endereco")]
        public string? Endereco { get; set; }

        [JsonProperty("Numero")]
        public string? Numero { get; set; }

        [JsonProperty("Complemento")]
        public string? Complemento { get; set; }

        [JsonProperty("Cep")]
        public string? Cep { get; set; }

        [JsonProperty("Node")]
        public string? Node { get; set; }

        [JsonProperty("Bairro")]
        public string? Bairro { get; set; }

        [JsonProperty("Pacote")]
        public string? Pacote { get; set; }

        [JsonProperty("Cod")]
        public string? Cod { get; set; }

        [JsonProperty("Telefone1")]
        public string? Telefone1 { get; set; }

        [JsonProperty("Telefone2")]
        public string? Telefone2 { get; set; }

        [JsonProperty("Obs")]
        public string? Obs { get; set; }

        [JsonProperty("ObsTecnico")]
        public string? ObsTecnico { get; set; }

        [JsonProperty("Equipamento")]
        public string? Equipamento { get; set; }

        public Document() { }
        
        public Document(
           DateTime? data,
           string? status,
           string? auditado,
           string? copReverteu,
           string? log,
           string? pdf,
           string? foto,
           string? contrato,
           string? wo,
           string? os,
           string? assinante,
           string? tecnicos,
           string? login,
           string? matricula,
           string? cop,
           string? ultimoAlterar,
           string? local,
           string? pontoCasaApt,
           string? cidade,
           string? @base,
           DateTime? horario,
           string? segmento,
           string? servico,
           string? tipoDeServico,
           string? tipoOs,
           string? grupoDeServico,
           string? endereco,
           string? numero,
           string? complemento,
           string? cep,
           string? node,
           string? bairro,
           string? pacote,
           string? cod,
           string? telefone1,
           string? telefone2,
           string? obs,
           string? obsTecnico,
           string? equipamento)
        {
            Data = data;
            Status = status;
            Auditado = auditado;
            CopReverteu = copReverteu;
            Log = log;
            Pdf = pdf;
            Foto = foto;
            Contrato = contrato;
            Wo = wo;
            Os = os;
            Assinante = assinante;
            Tecnicos = tecnicos;
            Login = login;
            Matricula = matricula;
            Cop = cop;
            UltimoAlterar = ultimoAlterar;
            Local = local;
            PontoCasaApt = pontoCasaApt;
            Cidade = cidade;
            Base = @base;
            Horario = horario;
            Segmento = segmento;
            Servico = servico;
            TipoDeServico = tipoDeServico;
            TipoOs = tipoOs;
            GrupoDeServico = grupoDeServico;
            Endereco = endereco;
            Numero = numero;
            Complemento = complemento;
            Cep = cep;
            Node = node;
            Bairro = bairro;
            Pacote = pacote;
            Cod = cod;
            Telefone1 = telefone1;
            Telefone2 = telefone2;
            Obs = obs;
            ObsTecnico = obsTecnico;
            Equipamento = equipamento;
        }
    }
}
