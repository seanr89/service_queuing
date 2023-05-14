
public record EmailContent{
    public string EmailAddress { get; set; }
    public string Header { get; set; }
    public string Content { get; set; }
    public bool Active { get; set; }

    public override string ToString()
    {
        var res = $"Address: {EmailAddress} with Header: {Header}";
        return res;
    }
}