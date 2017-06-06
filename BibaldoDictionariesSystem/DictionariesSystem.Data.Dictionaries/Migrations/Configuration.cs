namespace DictionariesSystem.Data.Dictionaries.Migrations
{
    using DictionariesSystem.Models.Dictionaries;
    using DictionariesSystem.Models.Dictionaries.Enums;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DictionariesSystem.Data.Dictionaries.DictionariesDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DictionariesDbContext context)
        {
            this.SeedDictionaries(context);
            this.SeedWords(context);
        }

        private void SeedDictionaries(DictionariesDbContext context)
        {
            if (context.Dictionaries.Any())
            {
                return;
            }

            var englishLanguage = new Language { Name = "English" };

            var englishDictionary = new Dictionary
            {
                Author = "Bardo",
                Language = englishLanguage,
                Title = "Bibaldo English Dictionary"
            };

            var bulgarianLanguage = new Language { Name = "Bulgarian" };

            var bulgarianDictionary = new Dictionary
            {
                Author = "Bibaldo",
                Language = bulgarianLanguage,
                Title = "Bibaldo Bulgarian Dictionary"
            };

            context.Dictionaries.AddOrUpdate(x => x.Id, englishDictionary, bulgarianDictionary);
        }

        private void SeedWords(DictionariesDbContext context)
        {
            if (context.Words.Any())
            {
                return;
            }

            var englishDictionary = context.Dictionaries.FirstOrDefault(x => x.Language.Name == "English");

            var originWord = new Word
            {
                Name = "Origin",
                SpeechPart = SpeechPart.Noun,
                Dictionary = englishDictionary
            };

            var begginingWord = new Word
            {
                Name = "Beginning",
                SpeechPart = SpeechPart.Noun,
                Dictionary = englishDictionary
            };

            var cradeWord = new Word
            {
                Name = "Crade",
                SpeechPart = SpeechPart.Noun,
                Dictionary = englishDictionary
            };

            var meaning = new Meaning
            {
                Description = "the point or place where something begins, arises, or is derived."
            };

            begginingWord.Meanings.Add(meaning);
            originWord.Meanings.Add(meaning);
            cradeWord.Meanings.Add(meaning);

            var footballWord = new Word
            {
                Name = "Football",
                SpeechPart = SpeechPart.Noun,
                Dictionary = englishDictionary,
            };

            var soccerWord = new Word
            {
                Name = "Soccer",
                SpeechPart = SpeechPart.Noun,
                Dictionary = englishDictionary,
            };

            var footMeaning = new Meaning
            {
                 Description = "the lower extremity of the leg below the ankle, on which a person stands or walks"
            };

            var footWord = new Word
            {
                 Name = "Foot",
                 SpeechPart = SpeechPart.Noun,
                 Dictionary = englishDictionary,
            };

            footballWord.Meanings.Add(footMeaning);
            footWord.ChildWords.Add(footballWord);

            var footballMeaning = new Meaning
            {
                Description = "any of various forms of team game involving kicking",
            };

            context.SaveChanges();
            context.Words.Add(begginingWord);
            context.Words.Add(originWord);
            context.Words.Add(cradeWord);
            context.Words.Add(footballWord);
            context.Words.Add(soccerWord);
            context.Words.Add(footWord);

            context.SaveChanges();
        }
    }
}
