namespace FluxoCaixa.Api.Adapters
{
    public class ModeloSaida
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string[] Produtos { get; set; }
    }

    public class ModeloInterno
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string[] Produtos { get; set; }
    }

    public class ModeloExterno
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string[] Produtos { get; set; }
    }
    public abstract class Adapter<T>  
    {

    }

    public partial class ModeloSaidaAdapter: Adapter<ModeloSaida>
    {
        public Adapter<ModeloSaida> InternoToModel(ModeloInterno modelo)
        {
            return this;
        }
        public Adapter<ModeloSaida> ExternoToModel(ModeloExterno modelo)
        {
            return this;
        }
    }


    public interface IWallet
    {
        public string GetCarteirasAsync(string id, string seguimento);
    }
    public interface IExterna
    {
        public string GetCarteirasExternaAsync(string id, string seguimento, string canal);
    }

    public interface IWalletAdapter
    {
        public string Executar(string id, string seguimento);
        public string Executar(string id, string seguimento, string canal);
    }

    public class WalletAdapter : IWalletAdapter
    {
        private readonly IWallet _wallet;
        private readonly IExterna _externa;

        public WalletAdapter(IWallet wallet, IExterna externa)
        {
            _wallet = wallet;
            _externa = externa;
        }

        public string Executar(string id, string seguimento)
        {
            return _wallet.GetCarteirasAsync(id, seguimento);
        }

        public string Executar(string id, string seguimento, string canal)
        {
            return _externa.GetCarteirasExternaAsync(id, seguimento, canal);
        }

    }

    public class Programa
    {
        private readonly IWalletAdapter _adapter;

        public Programa(IWalletAdapter adapter)
        {
            _adapter = adapter;
        }

        public string BuscarCarteiraInterna(string id, string seguimento)
        {
            var modeloSaida = new ModeloSaidaAdapter();
            modeloSaida.InternoToModel(new ModeloInterno());
            return _adapter.Executar(id, seguimento);
        }
        public string BuscarCarteiraExterna(string id, string seguimento, string canal)
        {
            var modeloSaida = new ModeloSaidaAdapter();
            modeloSaida.ExternoToModel(new ModeloExterno());
            return _adapter.Executar(id, seguimento, canal);
        }


    }

}
