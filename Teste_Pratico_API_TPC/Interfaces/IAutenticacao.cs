namespace Teste_Pratico_API_TPC.Interfaces
{
    public interface IAutenticacao
    {
        public string GenerateToken(string _email, string _senha);
    }
}