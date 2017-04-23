using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //var employeeRepository = new EmployeeRepository();

            //employeeRepository
            //    .GetId("").Match(
            //            failure: f => new InvalidOperationException(""),
            //            success: result => result);

            //WriteLine("Enter the first number: ");
            //var fst = ReadLine();

            //WriteLine("Enter the second number: ");
            //var snd = ReadLine();

            //Add(fst, snd).Match(
            //    success: result => WriteLine($"First + Second = {result}"),
            //    failure: WriteLine);

            //ReadLine();

        }

        public static Helper.Try<Exception, double> Add(string fst, string snd)
        {
            double f, s;

            if (!double.TryParse(fst, out f))
                return new ArgumentException($"Failed to parse '{fst}' to double.", nameof(fst));

            if (!double.TryParse(snd, out s))
                return new ArgumentException($"Failed to parse '{snd}' to double.", nameof(snd));

            return f + s;
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

    public class EmployeeRepository : IEmployeeRepository
    {
        public Helper.Try<Exception, Employee> GetId(string id)
        {
            if (string.IsNullOrEmpty(id))
                return new ArgumentException($"Failed to parse '{id}' to double.", nameof(id));

            return new Employee { };
        }
    }

    public interface IEmployeeRepository
    {
        Helper.Try<Exception, Employee> GetId(string id);
    }

    public class Employee
    {
        public string Id { get; set; }
    }
}
