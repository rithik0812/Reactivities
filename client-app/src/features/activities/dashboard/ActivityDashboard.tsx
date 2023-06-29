import { Grid, List } from 'semantic-ui-react';
import { Activity } from '../../../app/models/activity';
import ActivityList from '../../ActivityList';
import ActivityDetails from '../Details/ActivityDetails';
import ActivityForm from '../Form/ActivityForm';

interface Props {
    activities: Activity[];
    selectedActivity : Activity | undefined;
    selectActivity : (id : string) => void;
    cancelSelectedActivity : () => void;
    editMode : Boolean;
    openForm : (id : string) => void;
    closeForm : () => void;
    createOrEdit : (activity : Activity) => void;
    deleteActivity : (id : string) => void;
}

export default function ActivityDashboard({
    activities, 
    selectedActivity, 
    selectActivity, 
    cancelSelectedActivity, 
    editMode, 
    openForm, 
    closeForm, 
    createOrEdit,
    deleteActivity } : Props){

    return (
        // search grid layout for more info for the grid component (here we are using 16 columns))
        <Grid>
            <Grid.Column width='10'>
                <List>
                    <ActivityList 
                        activities={activities}
                        selectedActivity={selectedActivity}
                        selectActivity={selectActivity}
                        deleteActivity={deleteActivity}
                    />
                </List>
            </Grid.Column>
            <Grid.Column width='6'>
                {/* loads only if selectedActivity state has a value and not in editMode */}
                {selectedActivity && !editMode &&
                <ActivityDetails 
                    activity={selectedActivity} 
                    cancelSelectedActivity = {cancelSelectedActivity}
                    openForm={openForm}
                 />}
                {/* activates only when editMode state is true */}
                {editMode && 
                <ActivityForm 
                    activity={selectedActivity} 
                    closeForm={closeForm}
                    createOrEdit={createOrEdit}
                />}
            </Grid.Column>
        </Grid>
    )
}