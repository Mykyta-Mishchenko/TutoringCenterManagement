namespace JwtBackend.Models
{
    public class SignUpResult
    {
        public static SignUpResult Success => new SignUpResult() { Succeeded = true };
        public static SignUpResult Failure => new SignUpResult() { Failed = true };
        public static SignUpResult EmailFailure => new SignUpResult() { IsEmailAlreadyRegistered = true };
        public static SignUpResult Error(string error) => new SignUpResult() { Failed = true, ErrorMessage = error };

        private SignUpResult() { }

        public bool Succeeded { get; protected set; }
        public bool Failed { get; protected set; }
        public bool IsEmailAlreadyRegistered { get; protected set; }
        public string ErrorMessage { get; protected set; }

        public override string ToString()
        {
            return IsEmailAlreadyRegistered ? "EmailAlreadyRegistered" :
                   Succeeded ? "Succeeded" : "Failed";
        }
    }
}
