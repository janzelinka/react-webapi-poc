import { Box, Button, Modal, Paper, Typography } from '@mui/material';
import { JSX, useEffect, useState } from 'react';

interface ModalProps {
  form: JSX.Element;
  onOk: () => Promise<boolean>;
  onCancel: () => void;
  open: boolean;
  title: string;
}

export const CustomModal = ({
  form,
  open,
  onOk,
  onCancel,
  title,
}: ModalProps) => {
  const [visible, setVisible] = useState(open);

  useEffect(() => {
    setVisible(open);
  }, [open]);

  const _onOk = async () => {
    const result = await onOk();
    if (result) setVisible(false);
  };

  const _onCancel = () => {
    if (window.confirm('Your unsaved progress will be lost.')) {
      onCancel();
      // setVisible(false);
    }
  };

  return (
    <Modal
      open={visible}
      onClose={_onCancel}
      aria-labelledby="modal-modal-title"
      aria-describedby="modal-modal-description"
    >
      <Paper style={{ width: '50%', margin: '0 auto', marginTop: 200 }}>
        <Box padding={2}>
          <Typography variant="h5">{title}</Typography>
          {form}
          <Box display={'flex'} justifyContent={'end'}>
            <Button variant="contained" onClick={_onOk}>
              Ok
            </Button>
          </Box>
        </Box>
      </Paper>
    </Modal>
  );
};
