﻿namespace BoldChainBackendAPI.BoldChainModel.BoldChainDto
{
    public class AuthResponseDto
    {
        public string Email { get; set; }
        public string WalletAddress { get; set; }
        public string PublicKey { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }

}
