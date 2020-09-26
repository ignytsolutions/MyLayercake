namespace MyLayercake.Core.Exceptions {
    public class MyLayercakeException : System.Exception {
        public MyLayercakeException() {
        }

        public MyLayercakeException(string message)
            : base(message) {
        }

        public MyLayercakeException(string message, System.Exception ex)
            : base(message, ex) {
        }
    }
}
