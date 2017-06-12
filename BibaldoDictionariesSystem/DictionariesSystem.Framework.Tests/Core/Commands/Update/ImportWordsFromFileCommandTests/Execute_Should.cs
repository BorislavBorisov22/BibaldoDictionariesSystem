using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Contracts.Loaders;
using DictionariesSystem.Framework.Core.Commands.Update;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace DictionariesSystem.Framework.Tests.Core.Commands.Update.ImportWordsFromFileCommandTests
{
    [TestFixture]
    public class Execute_Should
    {
        [Test]
        public void CallWordsImporterFactoryWithCorrectFileType_WhenPassedParametersAreValid()
        {
            // arrange
            var importersFactory = new Mock<IWordsImporterFactory>();

            string fileType = "SomeFileType";
            string dictionaryTitle = "SomeTitle";
            string filePath = "SomePath";
            var commandParameters = new List<string>()
            {
              fileType,
              dictionaryTitle,
              filePath
            };

            var importer = new Mock<IWordsImporterProvider>();
            importersFactory
                .Setup(x => x.GetImporter(It.IsAny<string>()))
                .Returns(importer.Object);
      
            var importWordsFromFileCommand = new ImportWordsFromFileCommand(importersFactory.Object);

            // act
            importWordsFromFileCommand.Execute(commandParameters);

            // assert
            importersFactory.Verify(x => x.GetImporter(fileType), Times.Once);
        }

        [Test]
        public void CallTheTargetImportersImportMethodWithCorrectDictionaryTitleAndFilePath_WhenPassedParametersAreValid()
        {
            // arrange
            var importersFactory = new Mock<IWordsImporterFactory>();

            string fileType = "SomeFileType";
            string dictionaryTitle = "SomeTitle";
            string filePath = "SomePath";
            var commandParameters = new List<string>()
            {
              fileType,
              dictionaryTitle,
              filePath
            };

            var importer = new Mock<IWordsImporterProvider>();
            importersFactory
                .Setup(x => x.GetImporter(It.IsAny<string>()))
                .Returns(importer.Object);

            var importWordsFromFileCommand = new ImportWordsFromFileCommand(importersFactory.Object);

            // act
            importWordsFromFileCommand.Execute(commandParameters);

            // assert
            importer.Verify(x => x.Import(filePath, dictionaryTitle));
        }

        [Test]
        public void ReturnCorrectResultMessage_WhenPassedParametersAreValid()
        {
            // arrange
            var importersFactory = new Mock<IWordsImporterFactory>();

            string fileType = "SomeFileType";
            string dictionaryTitle = "SomeTitle";
            string filePath = "SomePath";
            var commandParameters = new List<string>()
            {
              fileType,
              dictionaryTitle,
              filePath
            };

            var importer = new Mock<IWordsImporterProvider>();
            importersFactory
                .Setup(x => x.GetImporter(It.IsAny<string>()))
                .Returns(importer.Object);

            var importWordsFromFileCommand = new ImportWordsFromFileCommand(importersFactory.Object);

            // act
            string resultMessage = importWordsFromFileCommand.Execute(commandParameters);

            // assert
            StringAssert.Contains($"from {filePath} have been successfuly imported", resultMessage);
        }
    }
}
