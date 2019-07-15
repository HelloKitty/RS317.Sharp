﻿using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using GladNet;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class OnConnectionEstablishedClaimSessionEventListener : BaseSingleEventListenerInitializable<INetworkConnectionEstablishedEventSubscribable>
	{
		private IPeerPayloadSendService<GameClientPacketPayload> SendService { get; }

		private IReadonlyAuthTokenRepository AuthTokenRepository { get; }

		private ICharacterDataRepository CharacterDataRepository { get; }

		public OnConnectionEstablishedClaimSessionEventListener(INetworkConnectionEstablishedEventSubscribable subscriptionService,
			[NotNull] IPeerPayloadSendService<GameClientPacketPayload> sendService,
			[NotNull] IReadonlyAuthTokenRepository authTokenRepository,
			[NotNull] ICharacterDataRepository characterDataRepository) 
			: base(subscriptionService)
		{
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
			AuthTokenRepository = authTokenRepository ?? throw new ArgumentNullException(nameof(authTokenRepository));
			CharacterDataRepository = characterDataRepository ?? throw new ArgumentNullException(nameof(characterDataRepository));
		}

		protected override void OnEventFired(object source, EventArgs args)
		{
			//Once connection to the instance server is established
			//we must attempt to claim out session on to actually fully enter.

			//TODO: This is temporary
			CharacterDataRepository.UpdateCharacterId(4);

			//TODO: When it comes to community servers, we should not expose the sensitive JWT to them. We need a better way to deal with auth against untrusted instance servers
			SendService.SendMessage(new ClientSessionClaimRequestPayload(AuthTokenRepository.RetrieveWithType(), 4));
		}
	}
}