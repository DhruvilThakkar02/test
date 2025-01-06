namespace HRMS.Utility.Helpers.SqlHelpers.Tenant
{
    public class TenantStoredProcedures
    {
        public const string GetTenants = "spTenantGetAll";
        public const string GetTenantById = "spTenantGet";
        public const string CreateTenant = "spTenantAdd";
        public const string UpdateTenant = "spTenantUpdate";
        public const string DeleteTenant = "spTenantDelete";
    }
}
