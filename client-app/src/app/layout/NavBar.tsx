import { Button, Container, Menu } from "semantic-ui-react";

interface Props {
    openForm : () => void; 
}

export default function NavBar({openForm} : Props) {
    return(
        <Menu inverted fixed='top'>
            <Container>

                <Menu.Item header>
                    <img src="/assets/logo.png" alt="logo" style={{marginRight: '10px'}}/>
                    Reactivities  
                </Menu.Item>

                <Menu.Item>
                    Activities
                </Menu.Item>

                <Menu.Item>
                    <Button onClick={openForm} postive content='Create Activity' />
                </Menu.Item>
                
            </Container>
        </Menu>
    ) 
}