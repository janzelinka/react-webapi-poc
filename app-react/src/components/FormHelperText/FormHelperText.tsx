import { FormHelperText } from '@mui/material';
import { FC } from 'react';

export const ValidationMessage: FC<{ text?: string }> = ({ text }) => {
  return <FormHelperText error={!!text}>{text}</FormHelperText>;
};
