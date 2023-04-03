using CI_Entity.Models;
using CI_Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PlatForm.Repository.Interface
{
    public interface IUserInterface
    {
        public bool Registration(string FirstName, string LastName, string Email, long PhoneNumber, string ConfirmPassword);
        public User Login(string Email, string Password);
        public User Forget(string Email);

        public PasswordReset passwordResets(string email, string token);
        //public PasswordReset AddPassToken(string email, string token);

        public List<User> user();
        public List<Mission> mission();
        public List<Country> countries();
        public List<City> cities();
        public List<Skill> skills();
        public List<MissionTheme> missionThemes();

        public List<GoalMission> goalMissions();
        public List<FavoriteMission> favoriteMissions();
        public List<MissionRating> MissionRatings();
        public List<MissionMedium> missionMedia();
        public List<Timesheet> timesheets();
        public List<Comment> comments();
        public MissionInvite AddMissionInvite(int FromUserId,int missionId,long Touserid);
        public List<MissionApplication> missionApplications();
        public FavoriteMission addfav(int missionId, int userId);
        public FavoriteMission FavMission(int missionId, int userId);

        public Comment addcomment(int missionId,int userId,string cmt);

        public MissionRating rating(int missionId, string starId,int userId);

        public MissionApplication applymission(int missionId, int userId);
       public List<Story> stories();
       public List<StoryMedium> storyMedia();
       public long SubmitStory(long missionId,long userId,string title,string description,DateTime date,long storyId);
       public void AddStoryMedia(string mediaType,string mediaPath,long missionId,long userId, long storyId,long sId);
       public long SaveStory(long missionId, long userId, string title, string description, DateTime date, long storyId);

        public void RemoveMedia(long stroryId); 
        public void cmtdetele(int cmtId, int userId);
        public void AddTime(long missionId, int userId, int? hour, int? min, int? action, DateTime date, string? notes);
        public void DeleteTimeSheet(int id);
    }
}
