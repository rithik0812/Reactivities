import react from 'react';
import { Button, Item, Label, Segment } from 'semantic-ui-react';
import { Activity } from '../app/models/activity';

interface Props
{
    activities: Activity[];
}

export default function ActivityList({activities}: Props) {
    return(
        <Segment>
            <Item.Group> 
            {activities.map((activitiy: Activity) => (
            // when looping over, each item must have a unique key
                <Item key={activitiy.id}> 
                    <Item.Content>
                        <Item.Header as='a'>{activitiy.title}</Item.Header>
                        <Item.Meta>{activitiy.date}</Item.Meta>
                        <Item.Description>
                            <div>{activitiy.description}</div>
                            <div>{activitiy.city}, {activitiy.venue}</div>
                        </Item.Description>
                        <Item.Extra>
                            <Button floated='right' content='View' color='blue'/>
                            <Label basic content={activitiy.category}/>
                        </Item.Extra>
                    </Item.Content>
                </Item>
            ))}
            </Item.Group>
        </Segment>
    )
}