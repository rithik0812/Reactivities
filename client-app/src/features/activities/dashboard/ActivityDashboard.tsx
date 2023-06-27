import react from 'react';
import { Grid, List } from 'semantic-ui-react';
import { Activity } from '../../../app/models/activity';
import ActivityList from '../../ActivityList';
import ActivityDetails from '../Details/ActivityDetails';

interface Props {
    activities: Activity[];
}

export default function ActivityDashboard({activities} : Props){
    return (
        // search grid layout for more info for the grid component (here we are using 16 columns))
        <Grid>
            <Grid.Column width='10'>
                <List>
                    <ActivityList activities={activities}/>
                </List>
            </Grid.Column>
            <Grid.Column width='6'>
                {/* loads only if activities[0] is avaliable, without this it crashes*/}
                {activities[0] &&
                <ActivityDetails activity={activities[0]} />}
            </Grid.Column>
        </Grid>
    )
}