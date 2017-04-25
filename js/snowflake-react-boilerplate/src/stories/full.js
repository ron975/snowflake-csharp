import React from 'react';

const style = {
  position: 'fixed',
  top: 0,
  left: 0,
  bottom: 0,
  right: 0,
  width: '100%',
  height: '100%'
};

export default function (storyFn) {
  return <div style={style}>{storyFn()}</div>;
}