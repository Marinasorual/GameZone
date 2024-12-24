namespace GameZone.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        private int maxLengthSizeInBytes;

        public MaxFileSizeAttribute()
        {
            _maxFileSize = FileSettings.MaxLengthSizeInBytes;
        }

        public MaxFileSizeAttribute(int maxLengthSizeInBytes)
        {
            this.maxLengthSizeInBytes = maxLengthSizeInBytes;
        }

        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                if (file.Length > _maxFileSize)
                {
                    var maxSizeInMB = FileSettings.MaxLengthSizeInMB;  
                    return new ValidationResult($"Max file size is: {maxSizeInMB} MB");
                }
            }
            return ValidationResult.Success;
        }
    }
}
