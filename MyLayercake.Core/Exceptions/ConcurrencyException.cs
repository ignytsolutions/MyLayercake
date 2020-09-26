namespace MyLayercake.Core.Exceptions {
    public class ConcurrencyException : MyLayercakeException {
        public ConcurrencyException(string message)
            : base(message) {
        }

        public ConcurrencyException(string message, System.Exception ex)
            : base(message, ex) {
        }
    }
}
