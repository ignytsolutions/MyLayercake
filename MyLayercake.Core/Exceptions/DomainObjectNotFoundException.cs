namespace MyLayercake.Core.Exceptions {
    public class DomainObjectNotFoundException : MyLayercakeException {
        public DomainObjectNotFoundException(string message)
            : base(message) {
        }

        public DomainObjectNotFoundException(string message, System.Exception ex)
            : base(message, ex) {
        }
    }
}
