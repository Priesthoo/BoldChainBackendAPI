﻿namespace BoldChainBackendAPI.BoldChainModel.BoldChainDto
{
    public class EmailAttachment
    {
        public string FileName { get; set; } = string.Empty;
        public byte[] Content { get; set; }
        public string ContentType { get; set; } = "application/octet-stream";
    }

}
