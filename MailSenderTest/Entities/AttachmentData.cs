namespace MailSender.Entities
{
    public class AttachmentData
    {
        public string? Name { get; set; }
        public string? Path { get; set; }
        public byte[]? File { get; set; }

        public AttachmentData(string name, byte[] file)
        {
            Name = name;
            File = file;
        }

        public AttachmentData(string path)
        {
            Path = path;
        }
    }
}
