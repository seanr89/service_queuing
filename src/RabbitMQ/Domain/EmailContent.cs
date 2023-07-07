
public record EmailContent{
    public string EmailAddress { get; set; }
    public string Header { get; set; }
    public string Content { get; set; }
    public bool Active { get; set; }
    public string Sender { get; set; } = "Default Sender";

    public override string ToString()
    {
        // var res = $"Address: {EmailAddress} from Sender: {Sender} with Header: {Header} and Content: {Content}";
        // return res;
        var res = $"Address: {EmailAddress} from Sender: {Sender}";
        return res;
    }
}