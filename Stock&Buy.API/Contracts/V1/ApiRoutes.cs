namespace Stock_Buy.API.Contracts.V1
{
    public static class ApiRoutes
    {
        private const string Root = "api";
        private const string Version = "v1";
        private const string Base = Root + "/" + Version;

        public static class Bundles
        {
            public const string GetAll = Base + "/bundles";
            public const string Get = Base + "/bundles/{id:Guid}";
            public const string Add = Base + "/bundles";
            public const string Update = Base + "/bundles/{id:Guid}";
            public const string Delete = Base + "/bundles/{id:Guid}";

            public const string AddAssociateBundles = Base + "/bundles/associatedBundle";
            public const string AddAssociateParts = Base + "/bundles/associatedPart";

            public const string UpdateAssociatedBundle = Base + "/bundles/associatedBundle/{id:Guid}";
            public const string UpdateAssociatedPart = Base + "/bundles/associatedPart/{id:Guid}";

            public const string GetMaxProductionAmount = Base + "/bundles/max/{id:Guid}";

        }

        public static class Parts
        {
            public const string GetAll = Base + "/parts";
            public const string Get = Base + "/parts/{id:Guid}";
            public const string Add = Base + "/parts";
            public const string Update = Base + "/parts/{id:Guid}";
            public const string Delete = Base + "/parts/{id:Guid}";
        }
    }
}
