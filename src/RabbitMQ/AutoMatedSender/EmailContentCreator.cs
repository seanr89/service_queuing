

using Bogus;

public static class EmailContentCreator
{
    public static EmailContent CreateBogusEmailContent()
    {
        var faker = new Faker<EmailContent>()
        .RuleFor(v => v.EmailAddress, f => f.Internet.Email())
        .RuleFor(v => v.Header, f => f.Lorem.Words(5).ToString())
        .RuleFor(v => v.Content, f => f.Lorem.Sentences(1))
        .RuleFor(v => v.Active, true);

        return faker.Generate();
    }
}