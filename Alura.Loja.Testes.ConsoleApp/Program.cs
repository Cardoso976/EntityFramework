using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Alura.Loja.Testes.ConsoleApp
{
    class Program
    {
        static void Main(String[] args)
        {
            var paoFrances = new Produto()
            {
                Nome = "Pão Francês",
                PrecoUnitario = 0.4,
                Categoria = "Padaria",
                Unidade = "Unidade"
            };

            var compra = new Compra()
            {
                Quantidade = 6,
                Produto = paoFrances
            };

            compra.Preco = paoFrances.PrecoUnitario * compra.Quantidade;

            using (var contexto = new LojaContext())
            {
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                contexto.Compras.Add(compra);

                ExibeEntries(contexto.ChangeTracker.Entries());

                contexto.SaveChanges();

            }         

        }
        private static void ExibeEntries(IEnumerable<EntityEntry> entries)
        {
            foreach (var e in entries)
            {
                Console.WriteLine(e.Entity.ToString() + " - " + e.State);
            }
        }
    }
}
