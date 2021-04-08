using System;
using System.Threading.Tasks;
using NSBus;

namespace One.API.dbo.Permission
{
    public class RolePermissions : IRolePermissions
    {
        private readonly SecurityContext _securityContext;

        public RolePermissions(SecurityContext securityContext)
        {
            _securityContext = securityContext;
        }

        public Task<bool> CanGetRoleDetails(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanCreateRole(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanDeleteRole(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanUpdateRole(Guid roleId)
        {
            return Task.FromResult(true);
        }
        public Task<bool> CanGetFund(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanCreateFund(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanUpdateFund(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanDeleteFund(Guid roleId)
        {
            return Task.FromResult(true);
        }
        public Task<bool> CanGetFundClass(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanCreateFundClass(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanUpdateFundClass(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanDeleteFundClass(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanCreateWatermark(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanUpdateWatermark(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanGetFundWaiver(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanCreateFundWaiver(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanDeleteFundWaiver(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanUpdateFundWaiver(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanGetProfileDetails(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanCreateProfile(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanUpdateProfile(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanDeleteProfile(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanGetProfileNotificationPreferenceDetails(Guid roleId, Guid profileId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanUpdateProfileNotificationPreference(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanCreateProfileNotificationPreference(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanDeleteProfileNotificationPreference(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanGetEmailNotificationTemplate(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanGetEmailNotifications(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanCreateClientEmailNotification(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanUpdateClientEmailNotification(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanDeleteClientEmailNotification(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanDeleteWatermark(Guid roleId)
        {
            return Task.FromResult(true);
        }
        public Task<bool> CanCreateSecurityQuestion(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanGetApprovalQueueDetails(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanUpdateDocumentApprovalStatus(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanCreateSecurityQuestions(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanGetUserDetailsList(Guid roleId)
        {
            return Task.FromResult(true); ;
        }

        public Task<bool> CanCreateUser(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanDeleteUser(Guid roleId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanAddRoleToUser(Guid roleId)
        {
            return Task.FromResult(true);
        }
    }
}
