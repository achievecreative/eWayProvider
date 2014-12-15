namespace AchieveCreative.eWayProvider.eWayLibs
{
    public class GetAccessCodeResultRequest
    {
        public GetAccessCodeResultRequest ( string accessCode )
        {
            AccessCode = accessCode;
        }

        public string AccessCode;
    }
}