

using Bogus;

public static class EmailContentCreator
{
    public static EmailContent CreateBogusEmailContent()
    {
        var faker = new Faker<EmailContent>()
        .RuleFor(v => v.EmailAddress, f => f.Internet.Email())
        .RuleFor(v => v.Header, f => f.Lorem.Word())
        .RuleFor(v => v.Content, f => f.Lorem.Sentences(1))
        .RuleFor(v => v.Active, true);

        return faker.Generate();
    }
    
    public static List<EmailContent> CreateBogusEmailContent(int count, string sender)
    {
        var faker = new Faker<EmailContent>()
        .RuleFor(v => v.EmailAddress, f => f.Internet.Email())
        .RuleFor(v => v.Sender, f => sender)
        .RuleFor(v => v.Header, f => f.Lorem.Word())
        .RuleFor(v => v.Content, f => f.Lorem.Sentences(1))
        .RuleFor(v => v.Active, true);

        return faker.Generate(count);
    }
}