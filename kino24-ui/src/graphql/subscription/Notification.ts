import { gql } from '@apollo/client';

export type NotificationSubscription = {
    notification: {
        id: string;
        type: string;
        payload: string; // JSON string
        timestamp: string;
    };
};

type User = {
    id: string;
    firstName: string;
    lastName: string;
};

export type NotificationCommentLikedPayload = {
    articleId: string;
    commentId: string;
    user: User;
};

export type NotificationSubscriptionVariables = {
    token: string;
};

export const NOTIFICATION_SUBSCRIPTION = gql`
    subscription Notifications($token: String!) {
        notification(token: $token) {
            id
            payload
            timestamp
            type
        }
    }
`;
