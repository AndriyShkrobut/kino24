import React, { useCallback } from 'react';
import { useSnackbar } from 'notistack';
import { OnSubscriptionDataOptions, useSubscription } from '@apollo/client';

import {
    NOTIFICATION_SUBSCRIPTION,
    NotificationCommentLikedPayload,
    NotificationSubscription,
    NotificationSubscriptionVariables,
} from 'graphql/subscription/Notification';
import { SNACKBAR_CONFIG } from 'constants/common';
import AuthStore from 'store/AuthStore';

const NotificationProvider: React.FC = ({ children }) => {
    const token = AuthStore.getToken() || '';
    const { enqueueSnackbar } = useSnackbar();
    const handleSubscriptionData = useCallback(
        (options: OnSubscriptionDataOptions<NotificationSubscription>) => {
            const {
                subscriptionData: { data },
            } = options;

            if (!data || !data.notification) return;

            const {
                notification: { payload },
            } = data;

            const notificationData = JSON.parse(payload) as NotificationCommentLikedPayload;
            const { user } = notificationData;

            const userName = `${user.firstName} ${user.lastName}`;
            enqueueSnackbar(`Your comment was liked by ${userName}`, SNACKBAR_CONFIG);
        },
        [enqueueSnackbar]
    );

    useSubscription<NotificationSubscription, NotificationSubscriptionVariables>(
        NOTIFICATION_SUBSCRIPTION,
        {
            onSubscriptionData: handleSubscriptionData,
            variables: { token },
            shouldResubscribe: true,
        }
    );

    return <React.Fragment>{children}</React.Fragment>;
};

export default NotificationProvider;
