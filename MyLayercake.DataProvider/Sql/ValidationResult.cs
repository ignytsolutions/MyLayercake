namespace MyLayercake.Sql {
    public class ValidationResult
    {
        private bool isSuccess = true;
        public bool IsSuccess
        {
            get { return isSuccess; }
            set { isSuccess = value; }
        }

        public string ErrorMessage { get; set; }

        public ValidationResult() { }

        public ValidationResult(bool success, string errorMessage) 
        {
            IsSuccess = success;
            ErrorMessage = errorMessage;
        }
    }
}
