using BasketballAcademy.Model;
using BasketballAcademy.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAcademy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminRepository Repository;

        public AdminController(IConfiguration configuration)
        {
            Repository = new AdminRepository(configuration);
        }


        string name;
        /// <summary>
        /// Retrieves a list of all administrators.
        /// </summary>
        /// <returns>List of Admin objects representing administrators.</returns>
        [HttpGet]
        [Route("ViewAdmin")]
        public List<Admin> ViewAdmin()
        {
            try
            {
                List<Admin> admins = Repository.ViewAdmin();
                return admins;
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                return new List<Admin>();
            }
        }

        /// <summary>
        /// Adds a new administrator.
        /// </summary>
        /// <param name="admin">Admin object containing information about the new administrator.</param>
        /// <returns>1 if the administrator is added successfully, 0 if the administrator already exists, -1 if an error occurs.</returns>
        [HttpPost]
        [Route("AddAdmin")]
        public int AddAdmin(Admin admin)
        {
            try
            {
                name = admin.fullName;
                logger.LogInfo("New admin " + name + " added", name);
                bool result = Repository.AddAdmin(admin);
                if (result)
                {
                    return 1; 
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                return -1; 
            }
        }

        /// <summary>
        /// Deletes an administrator by ID.
        /// </summary>
        /// <param name="id">ID of the administrator to be deleted.</param>
        /// <returns>Success message if the administrator is deleted, or an error message if an exception occurs.</returns>
        [HttpDelete]
        [Route("DeleteAdmin/{id}")]
        public string Delete(int id)
        {
            try
            {
                Repository.DeleteAdmin(id);
                return "Admin deleted successfully";
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                return "Something went wrong";
            }
        }

        /// <summary>
        /// Receives feedback from a user.
        /// </summary>
        /// <param name="contact">Contact object containing feedback information.</param>
        /// <returns>Success message if the feedback is sent successfully, or an error message if an exception occurs.</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Feedback")]
        public string Contact(Contact contact)
        {
            try
            {
                name = contact.Name;
                
                logger.LogInfo(name + " gives feedback about academy", name);
                Repository.Message(contact);
                return "Message sent successfully";
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                return "Something went wrong";
            }
        }

        /// <summary>
        /// Retrieves a list of feedback messages.
        /// </summary>
        /// <returns>List of Contact objects representing feedback messages.</returns>
        [HttpGet]
        [Route("ViewMessage")]
        public List<Contact> ViewMessage()
        {
            try
            {
                List<Contact> messages = Repository.ViewMessage();
                return messages;
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                return new List<Contact>();
            }
        }

        /// <summary>
        /// Deletes a feedback message by ID.
        /// </summary>
        /// <param name="id">ID of the feedback message to be deleted.</param>
        /// <returns>Success message if the feedback message is deleted, or an error message if an exception occurs.</returns>
        [HttpDelete]
        [Route("DeleteMessage/{id}")]
        public string DeleteMessage(int id)
        {
            try
            {
                Repository.DeleteMessage(id);
                return "Message deleted successfully";
            }
            catch (Exception exception)
            {
                logger.LogError(exception);
                return "Something went wrong";
            }
        }
    }
}

