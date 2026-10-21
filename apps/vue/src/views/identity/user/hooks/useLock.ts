import { useLocalization } from '/@/hooks/abp/useLocalization';
import { useModal } from '/@/components/Modal';
import { lock, unlock } from '/@/api/identity/users';
import { FormSchema } from '/@/components/Form';

export enum LockType {
  Seconds = 1,
  Minutes = 60,
  Hours = 3600,
  Days = 86400,
  Months = 2678400, // Calculated based on 31 days
  Years = 32140800, // Calculated based on 31*12 days
}

interface UseLock {
  emit: EmitType;
}

export function useLock({ emit }: UseLock) {
  const { L } = useLocalization('AbpIdentity');
  const [registerLockModal, { openModal }] = useModal();

  const formSchemas: FormSchema[] = [
    {
      field: 'seconds',
      component: 'Input',
      label: L('LockTime'),
      colProps: { span: 24 },
      required: true,
    },
    {
      field: 'type',
      component: 'Select',
      label: L('LockType'),
      colProps: { span: 24 },
      required: true,
      defaultValue: LockType.Seconds,
      componentProps: {
        options: [
          // TODO: localization
          { label: 'seconds', value: LockType.Seconds },
          { label: 'fen', value: LockType.Minutes },
          { label: 'hours', value: LockType.Hours },
          { label: 'day', value: LockType.Days },
          { label: 'month', value: LockType.Months },
          { label: 'Year', value: LockType.Years },
        ],
      },
    },
  ];

  function handleLock(id: string, input: { seconds: number; type: LockType }) {
    return lock(id, input.type * input.seconds).then(() => {
      emit('change');
    });
  }

  function handleUnLock(id: string) {
    return unlock(id);
  }

  function showLockModal(userId: string) {
    openModal(true, { userId }, true);
  }

  return {
    formSchemas,
    handleLock,
    handleUnLock,
    registerLockModal,
    showLockModal,
  };
}
