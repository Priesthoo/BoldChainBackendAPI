namespace BoldChainBackendAPI.BoldChainInterface
{
    public interface IEmailParser
    {
        //DKIM : (DomainKeys identified Mail) ensures an email was not tampered with and was sent from the authorized domain's mail server
        //by verifying a cryptographic signature in the email headers
        //If DKIM passes
        //You can trust the sender's domain
        //Boost Trust Score
        // Optionally show a 'verified' badge in UI
        string GetDkimHeader(string rawEmail);
    }
}
