using Rydo.Framework.Cache.Common;
using Rydo.Framework.Cache.Redis.Provider;
using Rydo.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Person> people = new List<Person>();

            for (int i = 0; i < 96000; i++)
            {
                people.Add(new Person(i, $"Person {i}", new List<Contact> { new Contact("1", "123456789"), new Contact("1", "234567890") }));
            }

            ICacheProvider cacheProvider = new RedisCacheProvider();

            cacheProvider.Set("PEOPLE", people);
            var contatos = cacheProvider.Get<List<Person>>("PEOPLE");

            var dataContext = new DapperDataContext();

            dataContext.ExecuteNonQueryText("INSERT INTO OfertaTipo ( IdTipoOferta, DescricaTipoOferta ) VALUES ( @idTipoOferta, @descricaTipoOferta )", new[]
            {
                new { idTipoOferta = 1, descricaTipoOferta = "Automatica" },
                new { idTipoOferta = 2, descricaTipoOferta = "Doadora" },
                new { idTipoOferta = 3, descricaTipoOferta = "Tomadora" }
            });

            dataContext.ExecuteNonQueryText("INSERT INTO OfertaTipoMovimento ( IdTipoMovimentoOferta, DescricaoTipoMovimentoOferta ) VALUES ( @idTipoMovimentoOferta, @descricaoTipoMovimentoOferta )", new[]
            {
                new { idTipoMovimentoOferta = 1, descricaoTipoMovimentoOferta = "Disponibilizar" },
                new { idTipoMovimentoOferta = 2, descricaoTipoMovimentoOferta = "Cancelar" },
            });
        }

        static Func<int, bool> IsPositivo = (numero) => { return numero > 0; };

        static Func<bool, bool> Where = (numero) => { return true; };

        public static Helper.Try<Exception, double> Add(string fst, string snd)
        {
            double f, s;

            if (!double.TryParse(fst, out f))
                return new ArgumentException($"Failed to parse '{fst}' to double.", nameof(fst));

            if (!double.TryParse(snd, out s))
                return new ArgumentException($"Failed to parse '{snd}' to double.", nameof(snd));

            return f + s;
        }

        public class Contact
        {
            public Contact(string type, string value)
            {
                Type = type;
                Value = value;
            }

            public string Type { get; private set; }
            public string Value { get; private set; }
        }

        public class Person
        {
            public Person(long id, string name, List<Contact> contacts)
            {
                Id = id;
                Name = name;
                Contacts = contacts;
            }

            public long Id { get; set; }
            public string Name { get; set; }
            public List<Contact> Contacts { get; set; }
        }

        public class FailedToParseDoubleException : Exception
        {
            public FailedToParseDoubleException(string message)
                : base(message) { }

            public override string ToString() => Message;
        }

        private static Helper.Try<FailedToParseDoubleException, double> ParseToDouble(Helper.Untrusted<string> source)
        {
            var result = 0d;

            return source.Validate(
                s => double.TryParse(s, out result),
                onFailure: s => new FailedToParseDoubleException($"Failed to parse '{s}' to double."),
                onSuccess: _ => result
            );

        }

        public class MimeTypeContext
        {
            public MimeTypeContext(IEnumerable<ItemMimeTypeContext> itemsContext)
            {
                ItemsContext = itemsContext;
            }

            public IEnumerable<ItemMimeTypeContext> ItemsContext { get; set; }
        }

        public class ItemMimeTypeContext
        {
            public ItemMimeTypeContext(UploadFileType uploadFileType, IEnumerable<string> mimeTypes)
            {
                UploadFileType = uploadFileType;
                MimeTypes = mimeTypes;
            }

            public ItemMimeTypeContext(UploadFileType uploadFileType)
                : this(uploadFileType, new List<string>())
            {

            }

            public UploadFileType UploadFileType { get; private set; }
            public IEnumerable<string> MimeTypes { get; private set; }
        }

        public enum UploadFileType : int
        {
            CustomerDocument = 1,
            CustomerProfile = 2,
        }
    }

    public class Perfil
    {
        public int IdPerfil { get; set; }
        public string DescricaoPerfil { get; set; }
    }
}
