import React, { useState, useEffect, Fragment } from 'react';
import axios from 'axios';
import { Container } from 'semantic-ui-react';
import { Activity } from '../models/activity';
import NavBar from './NavBar';
import ActivityDashboard from '../../features/activities/dashboard/ActivityDashboard';
import {v4 as uuid} from 'uuid';

function App() {
  const [activities, setActivities] = useState<Activity[]>([]);
  const [selectedActivity, setSelectedActivity] = useState<Activity | undefined>(undefined)
  const [editMode, setEditMode] = useState<Boolean>(false);

  // this function is trigggered whenever the app page re renders 
  // so its a componentDidMount + componentDidUpdate
  useEffect(() => {
    axios.get<Activity[]>('http://localhost:5000/api/activities')
      .then(response => {
        setActivities(response.data);
      })
  }, []);

  // find the activity by id, and set that activity to the selectedActivity state
  function handleSelectedActivity(id : string) {
    // finds the id matching the given id from the list of activities
    setSelectedActivity(activities.find(x => x.id === id));
  }

  // if canceled then return the selectedActivity state to undefined again
  function handleCancelActivity() {
    setSelectedActivity(undefined);
  }

  // the optional id param allow us to use the create activity separate from edit activity
  // if there is an id param we are in : view => edit 
  // if there is no id param we are in : create activity 
  // if we are in edit and we click create act, then handleCancelActivity is triggered
  // is we are in create act and we then view => edit, then handleSelectedActivity is triggered
  function handleFormOpen(id? : string){
    id ? handleSelectedActivity(id) : handleCancelActivity();
    setEditMode(true);
  }

  function handleFormClose(){
    setEditMode(false);
  }

  function handleCreateOrEditActivity(activity : Activity) {
    // if we are editing an activity then activity.id is present
    // if its present then replace the old instance of the activity with the edited one using destructuring
    // if its not present then add the new activity using destructuring
    activity.id
      ? setActivities([...activities.filter(x => x.id !== activity.id), activity])
      : setActivities([...activities, {...activity, id: uuid()}]) // uuid creates a new id, we destructure to implement the id

    setEditMode(false);
    setSelectedActivity(activity);
  }

  function handleDeleteActivity(id : string){
    setActivities([...activities.filter(x => x.id !== id)]); // filter everything except the id given and set state
  }

  return (
    // without a fragment or a div (or a <></>) we cant return 2 or more child components (NavBar and Container)
    <Fragment> 
      <NavBar openForm={handleFormOpen}/>
      <Container style= {{marginTop: '7em'}}>
        <ActivityDashboard 
        activities={activities}
        selectedActivity={selectedActivity}
        selectActivity={handleSelectedActivity}
        cancelSelectedActivity={handleCancelActivity}
        editMode={editMode}
        openForm={handleFormOpen}
        closeForm={handleFormClose}
        createOrEdit={handleCreateOrEditActivity}
        deleteActivity={handleDeleteActivity}
        />
      </Container>  
    </Fragment>
  );
}

export default App;
