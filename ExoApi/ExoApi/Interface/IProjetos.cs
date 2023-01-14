using ExoApi.Models;

namespace ExoApi.Interface
{
    public interface IProjetos
    {
        List<Projetos> Ler();
        void Cadastrar(Projetos projetos);
        void Atualizar(int IdProjeto, Projetos projetos);
        void Deletar(int IdProjeto);
        Projetos BuscarPorId(int IdProjeto);

    }
}
