function varargout = sp_demo(varargin)
% SP_DEMO MATLAB code for sp_demo.fig
%      SP_DEMO, by itself, creates a new SP_DEMO or raises the existing
%      singleton*.
%
%      H = SP_DEMO returns the handle to a new SP_DEMO or the handle to
%      the existing singleton*.
%
%      SP_DEMO('CALLBACK',hObject,eventData,handles,...) calls the local
%      function named CALLBACK in SP_DEMO.M with the given input arguments.
%
%      SP_DEMO('Property','Value',...) creates a new SP_DEMO or raises the
%      existing singleton*.  Starting from the left, property value pairs are
%      applied to the GUI before sp_demo_OpeningFcn gets called.  An
%      unrecognized property name or invalid value makes property application
%      stop.  All inputs are passed to sp_demo_OpeningFcn via varargin.
%
%      *See GUI Options on GUIDE's Tools menu.  Choose "GUI allows only one
%      instance to run (singleton)".
%
% See also: GUIDE, GUIDATA, GUIHANDLES

% Edit the above text to modify the response to help sp_demo

% Last Modified by GUIDE v2.5 10-Mar-2014 22:31:28

% Begin initialization code - DO NOT EDIT
gui_Singleton = 1;
gui_State = struct('gui_Name',       mfilename, ...
                   'gui_Singleton',  gui_Singleton, ...
                   'gui_OpeningFcn', @sp_demo_OpeningFcn, ...
                   'gui_OutputFcn',  @sp_demo_OutputFcn, ...
                   'gui_LayoutFcn',  [] , ...
                   'gui_Callback',   []);
if nargin && ischar(varargin{1})
    gui_State.gui_Callback = str2func(varargin{1});
end

if nargout
    [varargout{1:nargout}] = gui_mainfcn(gui_State, varargin{:});
else
    gui_mainfcn(gui_State, varargin{:});
end
% End initialization code - DO NOT EDIT


% --- Executes just before sp_demo is made visible.
function sp_demo_OpeningFcn(hObject, eventdata, handles, varargin)
% This function has no output args, see OutputFcn.
% hObject    handle to figure
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
% varargin   command line arguments to sp_demo (see VARARGIN)

% Choose default command line output for sp_demo
handles.output = hObject;

% Update handles structure
guidata(hObject, handles);

% UIWAIT makes sp_demo wait for user response (see UIRESUME)
% uiwait(handles.figure1);


% --- Outputs from this function are returned to the command line.
function varargout = sp_demo_OutputFcn(hObject, eventdata, handles) 
% varargout  cell array for returning output args (see VARARGOUT);
% hObject    handle to figure
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Get default command line output from handles structure
varargout{1} = handles.output;


% --- Executes on button press in btn_select_image.
function btn_select_image_Callback(hObject, eventdata, handles)
% hObject    handle to btn_select_image (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

%% Step 0: read a sample finger image
[filepath, filename] = uigetfile({'*.jpg;*.png;*.tif;*.bmp;*.gif;',...
    'Images (*.*.jpg,*.png,*.tif,*.bmp,*.gif)';...
    '*.*',  'All Files (*.*)'}, ...
    'Choose a file');
%filename = uigetfile('*', 'Select an image');
image = imread([filename, filepath]);
if size(image,3) > 1 % if image is not 8 bit
    image = rgb2gray(image);
end % convert image into grayscale
image = double(image);
colormap(gray);
imagesc(image,'Parent',handles.axe_original_image);
set(handles.axe_original_image,'DataAspectRatio',[1 1 1]);
% set(handles.axe_original_image,'XTick',[],'YTIck',[]});
axes(handles.axe_original_image);
zoom on;

%% Step 1: Gaussian filter + Segmentation
blksize = 8;
segThreshold = 0.25;
[ bgmask ] = segmentation(image, blksize, segThreshold);

% Downsize the original image
BN = 2;
bImage = imresize(image, 1/BN);
bgmask = imresize(bgmask, 1/BN);

imagesc(bImage.*bgmask,'Parent',handles.axe_segmentation);
set(handles.axe_segmentation,'DataAspectRatio',[1 1 1]);
axes(handles.axe_segmentation);

% Plot the boundary
boundaries = bwboundaries(bgmask);
numberOfBoundaries = size(boundaries);
hold on;
for k = 1 : numberOfBoundaries
    thisBoundary = boundaries{k};
    plot(thisBoundary(:,2), thisBoundary(:,1), 'g', 'LineWidth', 2);
end
hold off;
drawnow;

%% Step 2: compute Orientation Field
[Theta, gSquSqu] = orientation_field(bImage, 15, 15);
imagesc(bImage,'Parent',handles.axe_orientation_field);
set(handles.axe_orientation_field,'DataAspectRatio',[1 1 1]);
axes(handles.axe_orientation_field);
hold on;
R = 0.5;
quiver(R*cos(Theta),R*sin(Theta),...
     'Autoscale','off','color','g');
hold off;
drawnow;

%% Step 3a: Poincare Index
curveLen = 2;
[pCore, pDelta] = poincare_index(Theta, bgmask, curveLen);
imagesc(bImage.*bgmask,'Parent',handles.axe_poincare_index);
set(handles.axe_poincare_index,'DataAspectRatio',[1 1 1]);
axes(handles.axe_poincare_index);
hold on;
if (~isempty(pCore))
    plot( pCore(:,1), pCore(:,2),'ro','LineWidth',1);  
end
if (~isempty(pDelta))
    plot( pDelta(:,1), pDelta(:,2),'r^','LineWidth',1);
end
hold off;
drawnow;

%% Find max curves
curveLen = 3;
curves = max_curves(Theta, bgmask, curveLen);

imagesc(bImage.*bgmask,'Parent',handles.axe_bazen);
set(handles.axe_bazen,'DataAspectRatio',[1 1 1]);
axes(handles.axe_bazen);
hold on;
if (~isempty(pCore))
    plot( curves(:,1), curves(:,2),'ro','LineWidth',1);  
end
hold off;
drawnow;

 %% Step 3b: Bazen
%%coreThreshold = 6;
%%deltaThreshold = 6;
%%blockLen = 6;
%%[bCore, bDelta, gThetaSqu] = bazen(Theta,bgmask,blockLen,coreThreshold,deltaThreshold);
%%imagesc(bImage.*bgmask,'Parent',handles.axe_bazen);
%%set(handles.axe_bazen,'DataAspectRatio',[1 1 1]);
%%axes(handles.axe_bazen);
%%hold on;
%%if (~isempty(bCore))
%%plot( bCore(:,1), bCore(:,2),'ro','LineWidth',1);
%%end
%%if (~isempty(bDelta))
%%plot( bDelta(:,1), bDelta(:,2),'r^','LineWidth',1);
%%end
%%hold off;

%% Sten 3c: Bazen + filters
imagesc(bImage.*bgmask,'Parent',handles.axe_bazen_filters);
set(handles.axe_bazen_filters,'DataAspectRatio',[1 1 1]);
axes(handles.axe_bazen_filters);

% Arch
if (isempty(bCore) && isempty(bDelta))
edgeThreshold = 15;
[bCore] = arch(gThetaSqu, bgmask, edgeThreshold);
end

% C2D2
[ bCore, bDelta ] = c2d2( bCore, bDelta, bgmask );

% CCDis
if(size(bCore,1)==2)
ccDisThreshold = 100;
[ bCore ] = ccdis( bCore, ccDisThreshold, bgmask );
end

% CCOri
if(size(bCore,1)==2)
ccOriThreshold = 1.5;
[ bCore ] = ccori( bCore,  gSquSqu, ccOriThreshold, bgmask );
end

% DDOri
if(size(bDelta,1)==2)
ddOriThreshold = 0.6;
[ bDelta ] = ddori( bDelta,  gSquSqu, ddOriThreshold, bgmask );
end

% DCDAngle
if(size(bDelta,1)==2 && size(bDelta,1)>=1)
    maxAngle = 2;
    minAngle = 1;
    [ bDelta ] = dcdangle( bCore, bDelta, minAngle, maxAngle, bgmask);
end

hold on;
[xForeground, yForeground] = find(bgmask~=0);
xCenter = mean(xForeground);
yCenter = mean(yForeground);
plot(yCenter, xCenter, 'rx','LineWidth',10);
if (~isempty(bCore))
plot( bCore(:,1), bCore(:,2),'ro','LineWidth',1);
end
if (~isempty(bDelta))
plot( bDelta(:,1), bDelta(:,2),'r^','LineWidth',1);
end
hold off;
