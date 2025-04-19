import { PureComponent } from 'react';

import Grid from '@mui/material/Grid';
import Paper from '@mui/material/Paper';

import Chart from './Chart';
import Deposits from './Deposits';
import Orders from './Orders';
import Checkout from './components/Wizard/Checkout';

import { PieChart, Pie, ResponsiveContainer } from 'recharts';

export default function Dashboard() {
  return (
    <Grid container spacing={3}>
      {/* Chart */}
     <Grid size={{xs: 12, md: 8, lg: 9}}>
      
        <Paper
          sx={{
            p: 2,
            display: 'flex',
            flexDirection: 'column',
            height: 240,
          }}
        >
          <Chart />
        </Paper>
      </Grid>
      {/* Recent Deposits */}
      <Grid  size={{xs: 12, md: 4, lg: 3}}>
        <Paper
          sx={{
            p: 2,
            display: 'flex',
            flexDirection: 'column',
            height: 240,
          }}
        >
          <Deposits />
        </Paper>
      </Grid>
      {/* Recent Orders */}
      <Grid size={{xs: 12}}>
        <Paper sx={{ p: 2, display: 'flex', flexDirection: 'column' }}>
          <Orders />
        </Paper>
      </Grid>
      <Grid size={{xs:12}}>
        <Checkout />
      </Grid>
      <Grid size={{xs:12}}>
        <Example />
      </Grid>
    </Grid>
  );
}

const data01 = [
  { name: 'Group A', value: 400 },
  { name: 'Group B', value: 300 },
  { name: 'Group C', value: 300 },
  { name: 'Group D', value: 200 },
];

class Example extends PureComponent {
  static demoUrl = 'https://codesandbox.io/s/pie-chart-of-two-levels-gor24';

  render() {
    return (
      <ResponsiveContainer width="100%" height="100%">
        <PieChart width={400} height={400}>
          <Pie
            data={data01}
            dataKey="value"
            cx="50%"
            cy="50%"
            outerRadius={60}
            fill="#8884d8"
          />
        </PieChart>
      </ResponsiveContainer>
    );
  }
}
