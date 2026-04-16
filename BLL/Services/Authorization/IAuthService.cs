using Kudde.Shared.DTO.Authentication;

namespace BLL.Services.Authorization
{
    public interface IAuthService
    {
        /// <summary>
        /// Registers a new farm owner. Creates UserAccount + Farm + FarmUser (OWNR) in one transaction.
        /// </summary>
        Task<AuthResultDTO> RegisterOwnerAsync(RegisterOwnerDTO dto);

        /// <summary>
        /// Registers a new user via invite code. Validates code, creates UserAccount + FarmUser, marks invite used.
        /// </summary>
        Task<AuthResultDTO> RegisterInvitedAsync(RegisterInvitedDTO dto);

        /// <summary>
        /// Validates credentials and returns user info + farm context for claim building.
        /// </summary>
        Task<AuthResultDTO> LoginAsync(LoginDTO dto);

        /// <summary>
        /// Generates a new invite code for a farm. Only OWNR or MNGR should call this.
        /// </summary>
        Task<InviteResultDTO> CreateInviteAsync(CreateInviteDTO dto);

        /// <summary>
        /// Returns all active (unused, not expired) invites for a farm.
        /// </summary>
        Task<List<InviteResultDTO>> GetActiveInvitesAsync(Guid farmId);
    }
}