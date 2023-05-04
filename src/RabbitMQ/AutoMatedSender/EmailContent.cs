
public record EmailContent{
    public string EmailAddress { get; set; }
    public string Header { get; set; }
    public string Content { get; set; }
    public bool Active { get; set; }
}