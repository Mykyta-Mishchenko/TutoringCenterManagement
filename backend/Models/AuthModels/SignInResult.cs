namespace JwtBackend.Models
{
    public class SignInResult
    {
        public static SignInResult Success => new SignInResult() { Succeeded = true };
        public static SignInResult Failure => new SignInResult() { Failed = true };
        public static SignInResult NotAllowed => new SignInResult() { IsNotAllowed = true };
        public static SignInResult Error(string error) => new SignInResult() { Failed = true, ErrorMessage = error };
        public static SignInResult SignedIn(SessionTokens tokens) => new SignInResult() { Succeeded = true, Tokens = tokens };

        private SignInResult() { }

        public bool Succeeded { get; protected set; }
        public bool Failed { get; protected set; }
        public bool IsNotAllowed { get; protected set; }
        public string ErrorMessage { get; protected set; }
        public SessionTokens Tokens { get; protected set; }

        public override string ToString()
        {
            return IsNotAllowed ? "IsNotAllowed" :
                   Succeeded ? "Succeeded" : "Failed";
        }
    }
}
