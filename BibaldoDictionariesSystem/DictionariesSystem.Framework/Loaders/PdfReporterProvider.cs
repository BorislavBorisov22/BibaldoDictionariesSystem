using Bytes2you.Validation;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Contracts.Loaders;
using DictionariesSystem.Models.Users;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace DictionariesSystem.Framework.Loaders
{
    public class PdfReporterProvider : IPdfReporterProvider
    {
        private const string PdfDestinationPath = "../../../UsersReport.pdf";
        private const string WindowsDestinationPath = "..\\..\\..\\UsersReport.pdf";

        private readonly IRepository<User> usersRepository;

        public PdfReporterProvider(IRepository<User> usersRepository)
        {
            Guard.WhenArgument(usersRepository, "usersRepository").IsNull().Throw();

            this.usersRepository = usersRepository;
        }

        public void CreateReport()
        {
            var document = new Document(PageSize.A4, 10, 10, 42, 35);
            document.AddTitle("Pdf report");
            document.AddSubject("List users and their badges");
            document.AddKeywords("Metadata, Pdf, Databases");
            document.AddCreator("iTextSharp 5.4.4");
            document.AddAuthor("Rosen Urkov");
            document.AddHeader("Header", ":)");

            var fileStream = new FileStream(PdfDestinationPath, FileMode.Create);
            var writer = PdfWriter.GetInstance(document, fileStream);

            document.Open();

            var titleFont = FontFactory.GetFont("Tahoma", 30f, BaseColor.GRAY);
            var title = new Paragraph("Users List", titleFont);

            title.IndentationLeft = 220;
            document.Add(title);

            this.usersRepository
                .All(x => true)
                .ToList()
                .ForEach(x =>
                {
                    var username = new Paragraph($"Username: {x.Username}");
                    document.Add(username);
                    var contributions = new Paragraph($"Contributions: {x.ContributionsCount}");
                    document.Add(contributions);
                    var badges = new Paragraph("Badges: \n");
                    document.Add(badges);

                    var list = new List();
                    list.IndentationLeft = 10f;
                    x.Badges.ToList().ForEach(y => list.Add(y.Name));
                    document.Add(list);

                    document.Add(new Chunk("\n"));
                });

            document.Close();
            fileStream.Close();
            writer.Close();

            Process.Start(WindowsDestinationPath);
        }
    }
}
