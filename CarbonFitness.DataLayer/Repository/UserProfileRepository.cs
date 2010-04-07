using System;
using System.Linq;
using CarbonFitness.Data.Model;
using SharpArch.Data.NHibernate;
using NHibernate.Linq;

namespace CarbonFitness.DataLayer.Repository {
    public class UserProfileRepository : NHibernateRepositoryWithTypedId<UserProfile, int>, IUserProfileRepository {
        public override UserProfile Get(int userProfileId) {
            throw new NotImplementedException("Did u mean to use Get By UserId?");
        }

        public UserProfile GetByUserId(int userId) {
            var query = from userProfile in Session.Linq<UserProfile>()
                where userProfile.User.Id == userId
                select userProfile;

            return query.FirstOrDefault();
        }

    }
}