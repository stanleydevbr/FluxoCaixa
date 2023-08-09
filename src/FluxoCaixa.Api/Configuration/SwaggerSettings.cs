namespace FluxoCaixa.Api.Configuration
{
    public class SwaggerSettings
    {
        private static SwaggerSettings _config { get; set; }
        public static SwaggerSettings GetInstance(string nomeSessao = "Swagger")
        {
            if (_config == null)
                _config = BaseSettings.Get<SwaggerSettings>(nomeSessao);
            return _config;

        }

        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public VersaoConfig Versao { get; set; } = new VersaoConfig();
        public string PrefixoRota { get; set; }
        public ContatoConfig Contato { get; set; } = new ContatoConfig();
        public LicencaConfig Licenca { get; set; } = new LicencaConfig();
        public SegurancaConfig Seguranca { get; set; } = new SegurancaConfig();
    }

    public class SegurancaConfig
    {
        public string Identificador { get; set; } = "Bearer";
        public string Descricao { get; set; }
        public string Autorizacao { get; set; } = "Authorization";
    }

    public class LicencaConfig
    {
        public string Nome { get; set; }
        public string Url { get; set; }
    }

    public class ContatoConfig
    {
        public string Nome { get; set; }
        public string Email { get; set; }
    }

    public class VersaoConfig
    {
        public string Formato { get; set; } = "'v'.VVV";
        public int Maior { get; set; } = 1;
        public int Menor { get; set; } = 0;
    }
}
