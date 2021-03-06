﻿using HAA.BusinessEntities;
using HAA.BusinessServices.Base;
using HAA.DataModel;
using HAA.DataModel.UnitOfWork;
using System;
using System.Configuration;
using System.Linq;

namespace HAA.BusinessServices
{
    public class TokenServices : ITokenServices
	{
		#region Private member variables.
		private readonly UnitOfWork _unitOfWork;
		#endregion

		#region Public constructor.
		/// <summary>
		/// Public constructor.
		/// </summary>
		public TokenServices(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		#endregion

		#region Public member methods.

		/// <summary>
		///  Function to generate unique token with expiry against the provided userId.
		///  Also add a record in database for generated token.
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public TokenEntity GenerateToken(int userId)
		{
			string token = Guid.NewGuid().ToString();
			DateTime issuedOn = DateTime.Now;
			DateTime expiredOn = DateTime.Now.AddMinutes(
			Convert.ToDouble(ConfigurationManager.AppSettings["AuthTokenExpiry"]));
			var tokendomain = new Token
			{
				Id = userId,
				AuthToken = token,
				IssuedOn = issuedOn,
				ExpiresOn = expiredOn
			};

			_unitOfWork.TokenRepository.Insert(tokendomain);
			_unitOfWork.Save();
			var tokenModel = new TokenEntity()
			{
				UserId = userId,
				IssuedOn = issuedOn,
				ExpiresOn = expiredOn,
				AuthToken = token
			};

			return tokenModel;
		}

		/// <summary>
		/// Method to validate token against expiry and existence in database.
		/// </summary>
		/// <param name="tokenId"></param>
		/// <returns></returns>
		public bool ValidateToken(string tokenId)
		{
			var token = _unitOfWork.TokenRepository.Get(t => t.AuthToken == tokenId && t.ExpiresOn > DateTime.Now);
			if (token != null && !(DateTime.Now > token.ExpiresOn))
			{
				token.ExpiresOn = token.ExpiresOn.AddMinutes(
				Convert.ToDouble(ConfigurationManager.AppSettings["AuthTokenExpiry"]));
				_unitOfWork.TokenRepository.Update(token);
				_unitOfWork.Save();
				return true;
			}
			return false;
		}

		/// <summary>
		/// Method to kill the provided token id.
		/// </summary>
		/// <param name="tokenId">true for successful delete</param>
		public bool Kill(string tokenId)
		{
			_unitOfWork.TokenRepository.Delete(x => x.AuthToken == tokenId);
			_unitOfWork.Save();
			var isNotDeleted = _unitOfWork.TokenRepository.GetMany(x => x.AuthToken == tokenId).Any();
			if (isNotDeleted) { return false; }
			return true;
		}

		/// <summary>
		/// Delete tokens for the specific deleted user
		/// </summary>
		/// <param name="userId"></param>
		/// <returns>true for successful delete</returns>
		public bool DeleteByUserId(int userId)
		{
			_unitOfWork.TokenRepository.Delete(x => x.Id == userId);
			_unitOfWork.Save();

			var isNotDeleted = _unitOfWork.TokenRepository.GetMany(x => x.Id == userId).Any();
			return !isNotDeleted;
		}

		#endregion
	}

}
