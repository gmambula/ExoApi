﻿using ExoApi.Models;

namespace ExoApi.Interface
{
    public interface IUsuario
    {
        List<Usuarios> Listar();
        void Cadastrar(Usuarios usuario);
        void Atualizar(int IdUsuario, Usuarios usuario);
        void Deletar(int IdUsuario);
        Usuarios BuscarPorId(int IdUsuario);
        Usuarios Login(string Email, string Senha);


    }
}
