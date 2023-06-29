import { Button, Form, Segment } from "semantic-ui-react";
import { Activity } from "../../../app/models/activity";
import { ChangeEvent, useState } from "react";

interface Props {
    activity : Activity | undefined;
    closeForm : () => void;
    createOrEdit : (activity : Activity) => void;
}

export default function ActivityForm({
    activity : selectedActivity, 
    closeForm, 
    createOrEdit} : Props){

    // this state is what will be displayed in the form
    // if we are editing then we will see the info of the selectedActivity
    // if we create a new activity then it will have an empty state
    const initialState = selectedActivity ?? {
        id: "",
        title: "",
        date: "",
        description: "",
        category: "",
        city: "",
        venue: "" 
    }

    const [activity, setActivity] = useState<Activity>(initialState);

    // this is the function to handle form submition
    function handleSubmit(){
        createOrEdit(activity);
    }

    function handleInputChange(event : ChangeEvent<HTMLInputElement | HTMLTextAreaElement>){
        // here name is the name in the form element (ie. title, desc ...)
        // and value is the user input into the form
        const { name, value } = event.target; // destructuring an object

        // here the [name] is used to sub the attribute being changed
        // we are essentially changing the partial state for that attribute in activity
        setActivity({...activity, [name] : value})
    }

    return (
        // the clearing part adjusts the 
        <Segment clearing>
            <Form onSubmit={handleSubmit} autoComplete='off' >
                <Form.Input placeholder='Title' name='title' value={activity.title} onChange={handleInputChange} />
                <Form.TextArea placeholder='Description' name='description' value={activity.description} onChange={handleInputChange} />
                <Form.Input placeholder='Category' name='category' value={activity.category} onChange={handleInputChange} />
                <Form.Input placeholder='Date' name='date' value={activity.date} onChange={handleInputChange} />
                <Form.Input placeholder='City' name='city' value={activity.city} onChange={handleInputChange} />
                <Form.Input placeholder='Venue' name='venue' value={activity.venue} onChange={handleInputChange} />
                <Button floated='right' positive type='submit' content='Submit'/>
                <Button onClick={closeForm} floated='right' type='button' content='Cancel'/>
            </Form>
        </Segment>
    )
}