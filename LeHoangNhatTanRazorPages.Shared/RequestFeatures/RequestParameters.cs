namespace LeHoangNhatTanRazorPages.Shared.RequestFeatures
{
    public abstract class RequestParameters
    {
        private int _pageNumber = 1;
        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set
            {
                if (value == 0 || value < 0)
                    _pageNumber = 1;
                else _pageNumber = value;
            }
        }

        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (value == 0 || value < 0)
                    _pageSize = 10;
                else _pageSize = value;
            }
        }

        public bool? TakeAll { get; set; } = false;

        public string? OrderBy { get; set; }

        public string? SearchTerm { get; set; }
    }

}
