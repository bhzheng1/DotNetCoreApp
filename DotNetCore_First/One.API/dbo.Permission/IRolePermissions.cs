using System;
using System.Threading.Tasks;

namespace One.API.dbo.Permission
{
    public interface IRolePermissions
    {
        Task<bool> CanGetRoleDetails(Guid roleId);
        Task<bool> CanCreateRole(Guid roleId);
        Task<bool> CanUpdateRole(Guid roleId);
        Task<bool> CanDeleteRole(Guid roleId);
        Task<bool> CanGetFund(Guid roleId);
        Task<bool> CanCreateFund(Guid roleId);
        Task<bool> CanUpdateFund(Guid roleId);
        Task<bool> CanDeleteFund(Guid roleId);
        Task<bool> CanGetFundClass(Guid roleId);
        Task<bool> CanCreateFundClass(Guid roleId);
        Task<bool> CanUpdateFundClass(Guid roleId);
        Task<bool> CanDeleteFundClass(Guid roleId);
        Task<bool> CanCreateWatermark(Guid roleId);
        Task<bool> CanUpdateWatermark(Guid roleId);
        Task<bool> CanGetFundWaiver(Guid roleId);
        Task<bool> CanCreateFundWaiver(Guid roleId);
        Task<bool> CanDeleteFundWaiver(Guid roleId);
        Task<bool> CanUpdateFundWaiver(Guid roleId);
        Task<bool> CanGetProfileDetails(Guid roleId);
        Task<bool> CanCreateProfile(Guid roleId);
        Task<bool> CanUpdateProfile(Guid roleId);
        Task<bool> CanDeleteProfile(Guid roleId);
        Task<bool> CanGetProfileNotificationPreferenceDetails(Guid roleId, Guid profileId);
        Task<bool> CanCreateProfileNotificationPreference(Guid roleId);
        Task<bool> CanUpdateProfileNotificationPreference(Guid roleId);
        Task<bool> CanDeleteProfileNotificationPreference(Guid roleId);
        Task<bool> CanGetEmailNotificationTemplate(Guid roleId);
        Task<bool> CanGetEmailNotifications(Guid roleId);
        Task<bool> CanCreateClientEmailNotification(Guid roleId);
        Task<bool> CanUpdateClientEmailNotification(Guid roleId);
        Task<bool> CanDeleteClientEmailNotification(Guid roleId);
        Task<bool> CanDeleteWatermark(Guid roleId);
        Task<bool> CanCreateSecurityQuestion(Guid roleId);
        // Task<bool> CanUpdateSecurityquestion(Guid roleId);

        Task<bool> CanGetApprovalQueueDetails(Guid roleId);
        Task<bool> CanUpdateDocumentApprovalStatus(Guid roleId);


        Task<bool> CanGetUserDetailsList(Guid roleId);
        Task<bool> CanCreateUser(Guid roleId);
        Task<bool> CanDeleteUser(Guid roleId);
        Task<bool> CanAddRoleToUser(Guid roleId);
    }
}