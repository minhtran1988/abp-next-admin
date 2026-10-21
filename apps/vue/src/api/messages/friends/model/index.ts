import { UserCard } from '../../model';

export interface UserFriend extends UserCard {
  friendId: string;
  black: boolean;
  specialFocus: boolean;
  dontDisturb: boolean;
  remarkName: string;
  /** Whether online, only used by ui projects */
  onLined: boolean;
}

export interface FriendCreateRequest {
  friendId: string;
}

export interface FriendAddRequest extends FriendCreateRequest {
  remarkName: string;
}

export interface GetMyFriendsRequest {
  sorting?: string;
}

export interface GetMyFriendsPagedRequest extends PagedAndSortedResultRequestDto {
  filter?: string;
}
