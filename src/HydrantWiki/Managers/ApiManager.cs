﻿using System;
using System.IO;
using HydrantWiki.Network;
using HydrantWiki.Objects;
using HydrantWiki.ResponseObjects;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace HydrantWiki.Managers
{
    public class ApiManager
    {
        private HWManager m_HWManager;

        public ApiManager(HWManager _manager)
        {
            m_HWManager = _manager;
        }

        /// <summary>
        /// Authenticates the user returning a user object with
        /// the username, display name, and an authorization token
        /// to be used on subsequent calls
        /// </summary>
        /// <param name="_username">Username.</param>
        /// <param name="_password">Password.</param>
        public User Authenticate(string _username, string _password)
        {
            HWRestRequest request = new HWRestRequest();
            request.Method = HWRestMethods.Post;
            request.Host = m_HWManager.PlatformManager.ApiHost;
            request.Path = "/api/authorize";
            request.Headers.Add("Username", _username);
            request.Headers.Add("Password", _password);

            var response = m_HWManager.PlatformManager.SendRestRequest(request);
            AuthenticationResponse responseObject =
                JsonConvert.DeserializeObject<AuthenticationResponse>(response.Body);

            if (responseObject.Success)
            {
                return responseObject.User;
            }

            return null;
        }

        /// <summary>
        /// Saves the tag tot he server
        /// </summary>
        /// <returns>A tag response object.</returns>
        /// <param name="_user">User.</param>
        /// <param name="_tag">Tag.</param>
        public TagResponse SaveTag(User _user, Tag _tag)
        {
            string body = JsonConvert.SerializeObject(_tag);

            HWRestRequest request = new HWRestRequest();
            request.Method = HWRestMethods.Post;
            request.Host = m_HWManager.PlatformManager.ApiHost;
            request.Path = "/api/tag";
            request.Headers.Add("Username", _user.Username);
            request.Headers.Add("AuthorizationToken", _user.AuthorizationToken);
            request.Body = body;

            var response = m_HWManager.PlatformManager.SendRestRequest(request);

            if (response.Status == HWResponseStatus.Completed)
            {
                TagResponse responseObject =
                    JsonConvert.DeserializeObject<TagResponse>(response.Body);

                return responseObject;
            } else {
                throw new Exception(response.ErrorMessage);
            }
        }

        /// <summary>
        /// Sends the image to the server.  Should be called after the Tag
        /// </summary>
        /// <returns>The tag image.</returns>
        /// <param name="_user">User.</param>
        /// <param name="_fileName">File name.</param>
        public TagResponse SaveTagImage(User _user, string _fileName)
        {
            HWRestRequest request = new HWRestRequest();
            request.Method = HWRestMethods.Post;
            request.Host = m_HWManager.PlatformManager.ApiHost;
            request.Path = "/api/image";
            request.Headers.Add("Username", _user.Username);
            request.Headers.Add("AuthorizationToken", _user.AuthorizationToken);

            HWFile file = new HWFile
            {
                Filename = Path.GetFileName(_fileName),
                FullPathFilename = _fileName
            };
            request.File = file;

            var response = m_HWManager.PlatformManager.SendRestRequest(request);
            TagResponse responseObject =
                JsonConvert.DeserializeObject<TagResponse>(response.Body);

            return responseObject;
        }

        /// <summary>
        /// Returns the number of tags that the current user has made
        /// </summary>
        /// <returns>The my tag count.</returns>
        /// <param name="_user">User.</param>
        public TagCountResponse GetMyTagCount(User _user)
        {
            HWRestRequest request = new HWRestRequest();
            request.Method = HWRestMethods.Get;
            request.Host = m_HWManager.PlatformManager.ApiHost;
            request.Path = "/api/tags/mine/count";
            request.Headers.Add("Username", _user.Username);
            request.Headers.Add("AuthorizationToken", _user.AuthorizationToken);

            var response = m_HWManager.PlatformManager.SendRestRequest(request);
            TagCountResponse responseObject =
                JsonConvert.DeserializeObject<TagCountResponse>(response.Body);

            return responseObject;
        }

        public HydrantQueryResponse GetHydrantsInCirle(
            User _user, double _latitude, double _longitude, double _radius)
        {
            string url = string.Format("/api/hydrants/circle/{0}/{1}/{2}", _latitude, _longitude, _radius);

            HWRestRequest request = new HWRestRequest();
            request.Method = HWRestMethods.Get;
            request.Host = m_HWManager.PlatformManager.ApiHost;
            request.Path = url;
            request.Headers.Add("Username", _user.Username);
            request.Headers.Add("AuthorizationToken", _user.AuthorizationToken);

            var response = m_HWManager.PlatformManager.SendRestRequest(request);
            HydrantQueryResponse responseObject =
                JsonConvert.DeserializeObject<HydrantQueryResponse>(response.Body);

            return responseObject;
        }

        public HydrantQueryResponse GetHydrantsInBox(
            User _user,
            double _minLatitude,
            double _maxLatitude,
            double _minLongitude,
            double _maxLongitude)
        {

            string url = string.Format("/api/hydrants/box/{0}/{1}/{2}/{3}", _maxLatitude, _minLatitude, _maxLongitude, _minLongitude);

            HWRestRequest request = new HWRestRequest();
            request.Method = HWRestMethods.Get;
            request.Host = m_HWManager.PlatformManager.ApiHost;
            request.Path = url;
            request.Headers.Add("Username", _user.Username);
            request.Headers.Add("AuthorizationToken", _user.AuthorizationToken);

            var response = m_HWManager.PlatformManager.SendRestRequest(request);
            HydrantQueryResponse responseObject =
                JsonConvert.DeserializeObject<HydrantQueryResponse>(response.Body);

            return responseObject;
        }

        public TagsToReviewResponse GetTagsToReview(
            User _user)
        {

            string url = string.Format("/api/review/tags");

            HWRestRequest request = new HWRestRequest();
            request.Method = HWRestMethods.Get;
            request.Host = m_HWManager.PlatformManager.ApiHost;
            request.Path = url;
            request.Headers.Add("Username", _user.Username);
            request.Headers.Add("AuthorizationToken", _user.AuthorizationToken);

            var response = m_HWManager.PlatformManager.SendRestRequest(request);
            TagsToReviewResponse responseObject =
                JsonConvert.DeserializeObject<TagsToReviewResponse>(response.Body);

            return responseObject;
        }

    }
}
